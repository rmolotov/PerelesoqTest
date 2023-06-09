﻿## Структура проекта
Проект разделен на три части:
* **Infrastructure** - сервисы, провайдеры, инсталлеры;
* **Meta** - меню, настройки и прочее (TODO);
* **Gameplay** - гаджеты, UI для управления ими.

Структура проекта перекомпонована согласно частям и функциональностям, а не по типу ассетов.
При таком подходе концентрация разработчика на одной текущей фиче или модуле поддерживается отдельной деректорией, содержащей все необходимые ассеты.
Поддерживается аккуратная организация решения в Rider / VS.

Сторонние плагины и библиотеки вынесены в папку `Libs`, кодовая база самого проекта - в `PerelesoqTest`.

В "устойчивых разделах" проекта, имеющих однонаправленные зависимости к сервисным и геймплейным модулям,
добавлены `AsssemblyDefenition`'ы для ускорения перекомпиляции при разработке.
В геймплейных модулях их использовать не стоит - будут кросс-зависимости.

Пересобраны префабы некоторых гаджетов для удобства и единообразия. Счетчик электроэнергии переверстан в WorldSpace Canvas.

## Пакеты и библиотеки
* **Addresables** - менеджмент ассетов и их динамическая загрузка;
* **InputSystem** - используется в `EventSystem` для UI и в будущем для `InputActions`;
* **Cinemachine** - для управления и переключения камер;
* **Zenject** - DI контейнер для управления зависимостями сервисов и модулей;
* **DOTween + PRO** - процедурные анимации гаджетов и UI-элементов;
* **TextMeshPro**;
* **SRDebugger** - плагин для отладки, консоль и читы в рантайме (активация - тройной тап в правом верхнем углу);
* **Odin Inspector** - расширения редактора и кастомных утилит.

## Архитектурные принципы
* Архитектура - **сервисный** подход
* Геймплей - **компонентный** подход
* Фабрики:
  - `LevelFactory` - для создания и конструирования игровых объектов (TODO), сейчас инициализируется (`Initialize`) из `LevelInstaller`;
  - `UIFactory` - для создания и конструирования UI-элементов (виджетов, UI-root'a, HUD'a) и проброса в них зависимостей.

Каждый сервис, реализующий некоторую ответственность в проекте, регистрируется в DI контейнере по
интерфейсу и передается как зависимость (_LSP_, _DIP_) в `конструктор`ы или в специальные методы `Construct` с атрибутом `Inject` для `MonoBehavior`'ов.

Некоторые сервисы (например - remote (TODO) или `GameStateMachine`) требуют инициализации, поэтому реализуют метод `Initialize` Zenject-интерфейса `IInitializable`.
Что отражено в их регистрации `BindInterfacesAndSelfTo` в инсталлере `InfrastructureInstaller` в `ProjectContext`.

Поведение игровых объектов в геймплее собирается из нескольких небольших компонентов (_SRP_). Компоненты настраиваются фабрикой при создании игрового объекта.
Фабрика при этом пользуется сервисами, полученными как зависимости. При необходимости может выполниться инъекция в создаваемый игровой объект. Для этого
фабрики получают DI-контейнер как зависимость (и только они, согласно принципам Zenject).

## GameStateMachine
Используется для управления жизненным циклом игры. Описание состояний (в порядке перехода):
1. `Bootstrap`. Инициализация и ожидание статичных данных игры
2. `LoadPogressState`. Загрузка прогрресса игрока. TODO, сейчас скипается.
3. `LoadMetaState`. Загрузка и переход к меню или мета-геймплею. TODO, сейчас скипается.
4. `LoadLevelState`. Загрузка уровня и его ресурсов. Инициализация мира, спавн гаджетов и UI.
5. `GameLoopState`. Взаимодействие с игровым миром. На выходе (рестарте) очищаются фабрики и освобождаются ресурсы.

## Сервисы
1. <details><summary><b>ILoggingService</b> - отдельный сервис логгирования сообщений и ошибок, чтобы не таскать везде UnityEngine</summary>

   * Реализация: `LoggingService`
   * Методы:
     * `LogMessage` - логгирование сообщения
       * params: `message` (string) текст сообщения, `sender` (object) - подсистема/сервис, отправившая сообщение, по умолчанию - null
     * `LogWarning` - логгирование предупреждения
       * params: `message` (string) текст предупреждения, `sender` (object) - подсистема/сервис, отправившая предупреждение, по умолчанию - null
     * `LogError` - логгирование ошибки
       * params: `message` (string) текст ошибки, `sender` (object) - подсистема/сервис, отправившая ошибку, по умолчанию - null
      </details>

2. <details><summary><b>IAssetProvider</b> - управление ассетами, инициализируемый</summary>
   
   * Реализация: `AddressableProvider`
   * Методы:
     * `Load<T>` - асинхронная загрузка и кеширование ассета типа `T where T : class`
       * param: `key` (string) ключ ассета
       * return: `Task<T>`
     * `LoadScene` - асинхронная загрузка сцены
       * params: `sceneName` (string) - ключ сцены, `mode` - режим загрузки сцены (Single/Addditive), по умолчанию - Single
       * return: `Task<SceneInstance>`
     * `Release` - освобождение ассета из памяти
       * param: `key` (string) ключ ассета
     * `CleanUp` - освобождение всех ассетов из памяти
   </details>

3. <details><summary><b>SceneLoader</b> - загрузка сцен и уровней</summary>

    * Реализация: `SceneLoader`, единственная, без интентерфейса
    * Методы:
      * `Load` - асинхронная загрузка сцены
        * params: `sceneName` (string) - ключ сцены, `onLoaded` (Action&lt;string&gt;) callback, вызываемый по окончанию загрузки, по умолчанию - null
        * return: `Task<SceneInstance>`
      * `LoadSet` - асинхронная загрузка сцен (слоев) уровня. Суффиксы ключей слоев захардкожены (TODO - конфиг)
        * params: `sceneName` (string) - ключ уровня, `onLoaded` (Action&lt;string&gt;) callback, вызываемый по окончанию загрузки, по умолчанию - null
        * return: `Task<List<SceneInstance>>`
   </details>

4. <details><summary><b>IStaticDataService</b> - (TODO) загрузка, хранение и предоставление статических данных (конфигов) для игровыхх объектов и элементов UI</summary>

    * Реализация: `StaticDataService`. Сейчас данные гаджетов сохранены в префабах, но могут быть изменены фабрикой с учетом данных от сервиса. 
    * Методы:
      * Foo
      * Bar
   </details>

## Code convention
Реализация каждого сервиса или компонента разбивается на небольшие методы с именами, характеризующими их поведение.
При знакомстве с классом можно прочитать набор таких методов-действий в `public`-членах класса и понять общую логику поведения этого класса.
Если необхоимо погрузиться в тонкости реализации - класс пролистывается до `private` методов и членов класса.

Порядок указания методов класса:
1. `Конструктор` или `Construct`-метод, отражающий **зависимости** класса.
2. `Public`-методы.
3. Коллбеки жизненного цикла Unity (`Awake`, `Start`, `OnDestroy`...)
4. `Private`-методы.

## Расширения редактора и инструменты
### Блокировка панелей `EditorWindowLock` (Editor/EditorExtensions):
* CTRL + SPACE - блокировка панели **Inspector**,
* CTRL + SHIFT + SPACE - блокировка панели **Project** 
* или через меню `Edit/Custom Shortcuts`.

### Custom editors and Inspectors:
* **Functions** (`Lamp`, `DoorDriver`, `Camera`) и `PowerSource` - отображение статуса и кнопок действий в редакторе.
* **Camera** имет индикатор LIVE / DISABLED.
* **PowerSource** можно отключить кнопкой `Interact` в инспекторе, тогда весь уровень обесточится и учет электроэнергии приостановится. 
