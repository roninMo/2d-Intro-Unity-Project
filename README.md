
# 2d-Intro-Unity-Project
Hierarchical State Machine for the player, and something similar for the enemy ai. 

`Bardent is the go to guide for learning State Machine boys: https://www.youtube.com/watch?v=OjreMoAG9Ec`

<br />

###### My Own Notes:
Implementing a hierarchichal state machine for the character
Movement/physics/attacks/dashes/crouching/sliding

Enemy ai state machine
Necromancer that spawns skelly boys, 

some new enemy ideas

Build all the artwork
Learn how to create solid backgrounds/animatedBackgrounds/props woven in throughout the tileset
2d Effects, lots of them for just about everything, also cutscenes 

How to create a ui, hud, main menu, inventory, and settings

---

# A Guide to Building Your Own
## Land and Air States
This goes through the basics of creating the state machine, and then two super states that create the main functionality for character controls.

### States/Quickstart on Understanding the Pattern
The state machine pattern untangles a whole bunch of logic for moving the character around and makes it a lot easier to build heaps of functionality for your character without creating clutter or tripping over your current functionality.

A good example of what I mean is:
* Say you have functionality implemented for moving your character around on the ground, and you want a different type of movement in the air. Building all the conditional logic for this is fine, until you then implement jumping, wall sliding, wall jumping, crouching, etc, all on top of that. `The Problem is:`

        The physics for your movement will override the logic of your wall jump, all the logic 
        tangles together into a cluttered mess. It's really annoying, and state machine stops that

You just create a file to hold or change the current state, and then files for the functionality of each of your states, making it a lot easier on both you and the system.

---

#### Understanding the State Machine Pattern
This should clear up a bunch of stuff, and as you build your own it will all come together and make a buncha sense.

###### The initial setup for state machines:
* `StateMachine script` for handling the state (this doesn't get attached to anything)
    - Holds the value of the current state your player is in (the functionality that is currently implemented on teh character)
    - Has a function to change the state (transition from one state to another)
    - Also has an init state, for when the state is first rendered (generally to idle)

<br />

* `Player script` to tie everything together (this is the only script attached to your character and like the hub of the state)
    - This bad boy holds a reference to all the different state scripts, and each of the states constructors hold reference to this (all chained together)
    - It holds the player inputs, which then get passed down into the states like a daisy chain
    - It holds all the main components (rigidbody, boxcolliders, etc, animations)
    - It also holds reference to functions you wanna call in multiple states (SetVelocity for when you're both on ground and in air)

<br />

* `PlayerState script` The root or foundation state
    - This is the hub state. The state machine just manages each of these, but PlayerState is the root of the tree. Everything extends from this base function, ex: PlayerState to a GroundState, which trees down into an Idle or a Move State. 
    - The functions it holds are the standard script functions, well they're mapped to the standard functions and passed down to each state, which then adds onto each of them.
    - The functions are `Enter()`, `Exit()`, `LogicUpdate()`, `PhysicsUpdate()`, and to reduce clutter I'd recommend a function called `DoChecks()`.
        - `Enter` is called whenever you change a state, also in `StateMachine` `InitializeState` when it's initially rendered.
        - `Exit` is called at the end of a state before it changes into a new state. Each of these functions are only called once
        - `LogicUpdate` is called every frame, aka the Update function. In the player script it's called in the Update function just once, and is chained down into each of the nested states.
        - `PhysicsUpdate` is called every physics step, we call it in the FixedUpdate function in the player script
        - `DoChecks` is called in the `PhysicsUpdate` and on `Enter`, and it's very handy for checking for collisions or whatever you should check for. For my GroundState I have a call in the `DoChecks` to check if the player is touching the ground, and that then gets passed into my Move and Idle state for if the player wants to jump.

<br />

###### What Does That Mean?
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    *Essentially the playerState `maps` to your `Unity Callback functions (Update, FixedUpdate, etc)` the way you would normally, then expands on the functionality you'd normally implement but with less clutter. Then each state holds the individual mechanics within the respective state, and adds onto those mapped functions. Stopping you from tangling actions like differing air and ground movement, jump forces with movement forces, etc. It keeps it clean and easy to read, and each state transitions just like the animation controller (The animation controller is also based on a state machine).*

<br />

- One last thing, these states are classes with `constructors`, and each of them grab all the base data stored from the player (Inputs, meta/saveData, colliders, rb components, etc). They're all chained together, and pass down the information from one source, making it very organized and easy to access.

        This makes the code easier to understand, hard to read at first, unifies tangled logic, 
        and is very fun to build when you start to get a good understanding of it.

---
### The Different States
#### Super States
A `Super State` is a state that's inherited by states. Move and Idle states would Inherit the ground state, and ground state holds the logic for if you wanna jump. This cleans up code, and the only states you transition to are the Idle/Move State.

* These are essentially super/parent classes, and as you go down in the states you'll find more unique functionality/mechanics

#### Sub States
A `Sub State` inherits from the super state, and are what states your player invokes. These are the move, idle, jump, ability mechanics that all implement the functionality individually to keep your code clean and organized.

### That's All
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    *Just Start by creating the State Machine, A player script, a player state script and a couple states to understand how all this stuff ties together!*


# But Wait, There's More!
## Pixel Art for Aspiring Autists!
![Yee haww](https://media.giphy.com/media/vFKqnCdLPNOKc/giphy.gif)

`In reality just go checkout AdamCYouncis and all of his pixel art class videos, also the rest of his videos, but here's good some references I found`

*Also, the only editor you should be building in is Aseprite, it's 100% the way to go*

### Squash and Stretch Fundamentals
![Squash/Stretch Reference](https://i.pinimg.com/originals/a6/83/63/a683630de95203052028c0702c4dff97.gif)

### Animation Easing Fundamentals
![Animation Easing Reference](https://64.media.tumblr.com/aa1ee58decd2fb3100c47429ea61f31d/tumblr_inline_ozd9ghhZz31qdiwz3_540.gifv)

### 1 Pixel Movement
![Pixel Movement Reference](https://i.pinimg.com/originals/97/0c/e7/970ce7651ac970c0bc11bf29804ca04c.gif)

### Idle Animation
![Idle Reference](https://steamuserimages-a.akamaihd.net/ugc/850474042999442728/E4EDF44AA0A99CEE973859CC153424B6AAB4EAB0/)

### Outlines
![Outlines Reference](https://i.pinimg.com/originals/bc/09/be/bc09beec48c73a76b44e667a5bdc315f.gif)

### Breaking Objects
![Breaking Objects Reference](https://i.pinimg.com/originals/2c/a0/f2/2ca0f2a129f609ffaf36e94bc67b276a.gif)

### Wall Slide-Kick Reference
![Wall Climb Reference](https://i.pinimg.com/originals/1d/12/36/1d1236767b5284d083f0406b04e73074.gif)

### Slide-Roll-Dash Reference
![Slide/Roll/Dash Reference](https://i.pinimg.com/originals/43/ef/b5/43efb5ee656e99313ae7c72d3831a705.gif)

### Run Cycle Example
![Run Example](https://i.pinimg.com/originals/4a/c2/17/4ac2171493af90e2359f99917d92ffc0.gif)

### Walk Cycle Example
![Walk Example](https://i.pinimg.com/originals/24/e3/60/24e3602cb471b3a4ea861827a28eef35.gif)

### Sword Attack Animation Example
![Sword Attack Example](https://i.pinimg.com/originals/fd/ca/c7/fdcac74bff054ccc050d5b6def925081.gif)


