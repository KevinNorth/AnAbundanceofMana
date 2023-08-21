Structure of the Scripts directory:

 - Calculators: Game logic functions that can be called anywhere in the stack as needed. To avoid
     this leading to coupling across layers, these are all implemented as pure functions with no
     side effects.
   - Calculators/CardLocation: Determines where cards are as well as which cards move between Actions.
   - Calculators/Score: Adds up the player's mana based on the current game state. Also produces intermediate
     results that the UI can use to help indicate how score is calculated.
 - Effects: Plain Old Code Objects that are used by spells, buffs, and cards to indicate the game logic
     effects they can incur. These effects do not actually implement their behavior; rather, Controllers
     reference Effect objects to determine which Actions to dispatch. This allows them to be referenced
     across layers of the stack without leading to coupling.
   - Effects/CardEffects: Effects that are triggered by cards being played, discarded, etc.
 - Managers: The MonoBehavior singletons that live in Unity scenes and provide references to the
     MVC, State, and Calculator patterns.
 - MVC: A Model/Controller/View pattern.
   - MVC/Controllers: Game logic implementations. Use the current State, Models information, and Actions to implement high- and mid-level behavior.
   - MVC/Models: Scriptable objects for cards, spells, and other entities.
   - MVC/Views: A representation of game state that can be fed easily to Renderers.
 - State: A Redux-style state tree pattern.
   - Actions: Used to communicate low-level game interactions between controllers and reducers.
   - Reducers: Consume actions to mutate state.
   - State: A mutable representation of game logic that is easy for Controllers to read.
   - Entities: In-memory representation of models and other game entities. These do not have Actions and Reducers per say, but States are composed of Entities and can be mutated without affecting the underlying persisted data.
 - UI: What it says.
   - UI/Elements: MonoBehaviors to drive the entities that appear in the Unity scene.
     - UI/Elements/Developer Facing: Unity entities that are used to provide developer-friendly debugging information.
       (I'm considering leaving them in final releases so that top-level players can use them for theorycrafting.)
   - UI/Events: Represent actions that the player can take or that game entities can incur. Despite the name,
     these are not C# or Unity events proper. They're simply Plain Old Code Objects that are consumed syncronously
     by Managers.
   - UI/Feedback: Plain Old Code Objects that Controllers can return to help Managers and Renderers update UI state
     decoupled from the State layer.
   - UI/Renderers: Take the Views emitted by the MVC pattern to control what is shown on screen.

The core idea behind this organization is that I want to be able to implement the game logic in a way
that is highly decoupled from the UI state. For example, the game logic has no reason to care about
where cards are physically located on the screen as plays are animated. And the UI does not need to
provide a representation of all minutia of card effects. So the overall stack looks like this:

 1. At the top, Unity objects are implemented with UI/Elements driving their behaviors.
 2. These behaviors rely on Managers and Renderers to coordinate their states.
 3. Managers feed player actions (which I call Events) into Controllers.
 4. Controllers use the player's Events and the Effects on cards, spells, and other game entities
    to dispatch Actions to the Store.
 5. The Store is the single source of truth for the current state of the game in terms of the game's rules.
    Actions are processed by Reducers to update the canonical State.
 ^4. Going back up the stack, Controllers collect Feedback about how the Store's State changed...
 ^3. ... to feed back to Managers.
 ^2. The Managers and Reducers then use this Feedback to update the UI state, allowing for things
     like animating card movement completely independently of the underlying logical State.
 ^1. This drives the behavior of Unity objects and what the player actually sees.
 +. Throughout the stack, Calculators are used as necessary to determine metadata that can always be
    derived directly from the State. This prevents decorating the State with metadata directly when it
    could go out of sync, keeping us with one easily-managed source of truth.
 +. Also throughout, Effects are used as metadata to control how cards, spells, and other game entities
    behave. Since Effects are Plain Old Code Objects incapable of causing side effects on their own,
    using them liberally at all levels of the stack is harmless, making it easy to use the same language
    to communicate these effects throughout the program.