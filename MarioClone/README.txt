SPRINT 1 README
The question and used block may not behave exactly as asked in the story. The description specifies that Xx should be used for the 
used block, which does nothing. It also specifies that ? be used for the question block. However, in the acceptance critera, it 
lists ? as a bonus and says Xx should be used for the question block bump. Then, in the Canvas message, it was specified that ? was 
bonus, and that / was the desired button for the question block. So, since the used block does nothing, but has a button mapped to it 
in the description, and the description says that for some blocks pressing their button will have no visible effect, it made sense that 
the used block should have the Xx keys mapped to it, since it's the only one that would have done nothing. 
So, the button configuration for blocks is: [Xx] for the used block (does nothing) [/?] for the question block, [Bb] for the brick 
block, and [Hh] for the hidden block.

Ignored code warnings:
Mark 'MarioClone.exe' with CLSCompliant(true) because it exposes externally visible types. 
-Not sure what this means or how to do it. Since it's just some external code flag, it doesn't seem important to do.

The 'this' parameter (or 'Me' in Visual Basic) of 'BlockFactory.Create(BlockType, Vector2)' is never used. Mark the member as 
static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.
-Don't want to do this in case block factory is later subclassed to provide different versions of block factory, in which case an
object would want to hold a reference to a particular kind of block factory. I think making the create static would push it out across
all block factory types.

The 'this' parameter (or 'Me' in Visual Basic) of 'EnemyFactory.Create(EnemyType, Vector2)' is never used. Mark the member as 
static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.
-Same reasoning as above for this factory.


SPRINT 2 README
This was not specified as *not* happening in the PBI, and it was an easy thing to include, so we made the Koopas deal damage to Mario
if he hits them on the sides, and change Mario's power-up state. Also, the "A" button is the Up "Direction" for the game pad. That was never
(to my knowledge) explicitly specified as having to be the up arrow, so I'm just leaving that as whatever someone else made it.
Finally, the Mario power-up responses are left unspecified in the Avatar Collision Response - "Power-Up Change" acceptance criteria. It explains
that hitting a mushroom while in standard should change Mario to super, but nothing about the state of Mario from other powerups. So, our
assumptions are that if Mario hits a fire flower while in any non-dead state, he becomes fire Mario, and if he hits a red mushroom while
in ANY non-dead state, he will become super (this could downgrade him from fire). The powerup state cheats still work the same.


Ignored code warnings:
Mark 'MarioClone.exe' with CLSCompliant(true) because it exposes externally visible types. 
-Not sure what this means or how to do it. Since it's just some external code flag, it doesn't seem important to do.

The 'this' parameter (or 'Me' in Visual Basic) of 'BlockFactory.Create(BlockType, Vector2)' is never used. Mark the member as 
static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.
-Don't want to do this in case block factory is later subclassed to provide different versions of block factory, in which case an
object would want to hold a reference to a particular kind of block factory. I think making the create static would push it out across
all block factory types.

The 'this' parameter (or 'Me' in Visual Basic) of 'EnemyFactory.Create(EnemyType, Vector2)' is never used. Mark the member as 
static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.
-Same reasoning as above for this factory.

'GameGrid.gameGrid' is a multidimensional array. Replace it with a jagged array if possible.
-It's not a jagged array, it's a multidimensional array. Each row/column is the same length as the other rows/columns, it's easier this way.

Implement IDisposable on 'HitBox' because it creates members of the following IDisposable types: 'Texture2D'. If 'HitBox' 
has previously shipped, adding new members that implement IDisposable to this type is considered a breaking change to existing consumers.
-Hitboxes stay for basically the entire game (right now), so implementing IDisposable would just be a mostly useless chore for now.

Implement IDisposable on 'LevelCreator' because it creates members of the following IDisposable types: 'Bitmap'. If 'LevelCreator' 
has previously shipped, adding new members that implement IDisposable to this type is considered a breaking change to existing consumers.
-Same as above


Mark 'MarioClone.exe' with CLSCompliant(true) because it exposes externally visible types.
-I don't even know what that means, but I can't figure out how to do it regardless.

The 'this' parameter (or 'Me' in Visual Basic) of 'BlockFactory.Create(BlockType, Vector2)' is never used. Mark the member as 
static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.
-Don't want to do this in case block factory is later subclassed to provide different versions of block factory, in which case an
object would want to hold a reference to a particular kind of block factory. I think making the create static would push it out across
all block factory types.

SPRINT 3 README

There are no major concerns for this sprint; however, there are some things I would like to point out. So when the mushrooms are within the viewport window
they are currently floating up before gravity makes them fall down. Also, we did add in a pipe but we have yet to add in a pirahna but I do not believe
it was explicitly listed in any stories for this sprint. 


Ignored code warnings:

Warnings ignored for same reasona as the previous sprints:
-CA1822 : 'this' parameter (or 'Me' in Visual Basic) of 'EnemyFactory.Create(EnemyType, Vector2)'
-CA1014: Mark 'MarioClone.exe' with CLSCompliant(true) because it exposes externally visible types.
-CA1001: Implement IDisposable on 'HitBox'
-CA1001: Implement IDisposable on 'LevelCreator'
-CA 1814: Replace gamegrid with jagged array


-Warning	CA1804	'CollisionManager.filterCollisions(List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>, List<Tuple<float, Side, AbstractGameObject, AbstractGameObject>>, out float)' declares a variable, 'side', of type 'Side', which is never used or is only assigned to. Use this variable or remove it.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Collision\CollisionManager.cs	452	Active
			-we will need this variable 

-Warning	CA1502	'CollisionManager.ProcessFrame(GameTime, List<AbstractGameObject>, GameGrid)' has a cyclomatic complexity of 57. Rewrite or refactor the method to reduce complexity to 25.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Collision\CollisionManager.cs	496	Active
			-our current implementation is working well and we need all of this logic for each type of gameobject represented to be represented in our bitmap

-Warning	CA1051	Because field 'BreakableBrickObject.PieceList' is visible outside of its declaring type, change its accessibility to private and add a property, with the same accessibility as the field has currently, to provide access to it.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\GameObjects\Bricks\BreakableBrickObject.cs	14	Active
			-its okay that it is not private and we need to keep it public for now 

-Warning    CA1002: These lists were never intended to be subclassed, and the CA1002 explanation on Microsoft's
website https://msdn.microsoft.com/en-us/library/ms182142.aspx says the following: 
"System.Collections.Generic.List<T> is a generic collection that is
designed for performance and not inheritance. 
System.Collections.Generic.List<T> does not contain virtual members 
that make it easier to change the behavior of an inherited class. 
The following generic collections are designed for inheritance and 
should be exposed instead of System.Collections.Generic.List<T>."
As a matter of fact, these lists were exactly intended for performance,
as they were what managed all the objects and sorted, searched, etc. the
collisions. The fact that they are passed around does not change the fact
that there was no intent to inherit them, and in fact they never appear
in an inheritable or inherited class.

Additional Ignores:
-CA2214: (There are ~38 of these): 
As Anna discussed in class with Scott, we're ignoring this because we could not devise a work around. 


SPRINT 4 README 

-When Mario is normal sized, he usually jumps right through the flagpole if you move him up + left. Thus this does not properly trigger the game to end. This can also happen
when he is in other states, but it is much more rare. He is able to collide with the flagpole better when he is not in normal state. Also, since the acceptance criteria only asks
for the corresponding height to add a certain amount to the score, currently we have almost simultaneously as Mario hits the pole, the winning screen appears. 

-To cycle through the menu commands, press SPACE, and to select an option, press ENTER.
-Score and coins reset on Mario death. 
-I have no idea why, but the underworld is *extremely* laggy on my laptop. This hasn't been replicable with other computers, so hopefully
you don't experience this bug.
-All the (brick) blocks have coins in them. This was something we simply forgot to take out from when we were testing stuff relating to making coins and collision.
-There's no warp pipe sound.
-Mario, after colliding with an enemy, becomes INVINCIBLE and will pass through enemies (Much like the original Mario) without harming them
or taking harm. This is an intended behavior, and I just wanted you to know it wasn't a bug.
-Finally, the level is a decent replica of the original Super Mario Bros World 1-1, enjoy.

-A lot of these issues (above) were fixable, but we simply ran out of time on this sprint.

Ignored code warnings:

Warnings ignored for same reasons as the previous sprints:
-CA1822 : 'this' parameter (or 'Me' in Visual Basic) of 'EnemyFactory.Create(EnemyType, Vector2)'	5 of the errors
-CA1014: Mark 'MarioClone.exe' with CLSCompliant(true) because it exposes externally visible types.		1 of the errors
-CA1001: Implement IDisposable on 'HitBox'		2 of the errors (level creator is never disposed, so we'd never write a method for it)
-CA 1814: Replace gamegrid with jagged array	1 of the errors
-CA2214: 'RedKoopaObject.RedKoopaObject(ISprite, Vector2)' contains a call chain that results in a call to a virtual method defined 
by the class. Review the following call stack for unintended consequences:		39 of the errors
	--this one was discussed with you, and we're just not sure how to fix it, same reason as before


1 of the errors
-- Warning	CA1030	Consider making 'Mario.FireBall()' an event.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\GameObjects\Mario.cs	146	Active
This one just doesn't make sense. Mario.Fireball() is a command to throw a fireball, not an event..

9 of the errors 
--Warning	CA1051	Because field 'BreakableBrickObject.PieceList' is visible outside of its declaring type, change its accessibility to private and add a property, with the same accessibility as the field has currently, to provide access to it.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\GameObjects\Bricks\BreakableBrickObject.cs	14	Active
10 of errors
--Warning	CA1002	Change 'List<AbstractGameObject>' in 'BreakableBrickObject.PieceList' to use Collection<T>, ReadOnlyCollection<T> or KeyedCollection<K,V>	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\GameObjects\Bricks\BreakableBrickObject.cs	14	Active
These kind of go together. A lot of random things, I believe all of them lists, give warnings because they are lists exposed outside of
their class. For the same reasons as given in the previous sprint, I'm not going to convert all of these to Collections. Also, on some
of the objects, the lists actually need to be public as they're accessed by other methods. If I changed them to a property, it would probably
just give CA1002 instead, gaining me a net reduction of 0 errors.

Severity	Code	Description	Project	File	Line	Suppression State
1--Warning	CA1809	'LevelCreator.MakeObject(Color, int, int)' has 69 local variables, 36 of which were generated by the compiler. Refactor 'LevelCreator.MakeObject(Color, int, int)' so that it uses fewer than 64 local variables.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Level\LevelCreator.cs	69	Active
Only a few of these variables would be instantiated at any given time, as the level creator is just a giant if statement. There are probably
better ways to create the objects than we currently are, but basically upon creation the object has to be adjusted around based on its sprite
sheet, since its pixel position on the bitmap game definition is not perfect. For that to happen, it has to be created (so its sprite isn't null)
before we determine its final position in the world. So, we have to create it, then set its position, then add it to the gamegrid. 


5 of the errors
--Warning	CA1026	Replace method 'Camera.Move(Vector2, bool)' with an overload that supplies all default arguments.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Cam\Camera.cs	90	Active
Several methods (Mostly draws) supply just a few default arguments. It seems silly that we can't (monogame does I believe), because some 
arguments are nearly always one particular value, the extra argument actually only exists to satisfy 1-2 particular classes in a lot of 
cases, while still fitting an abstract class or interface. Additionally, for the Camera one, I think that came with the code you gave us for
the camera.

3 of the errors
Warning	CA1502	'CollisionManager.ProcessFrame(GameTime, List<AbstractGameObject>, GameGrid)' has a cyclomatic complexity of 58. Rewrite or refactor the method to reduce complexity to 25.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Collision\CollisionManager.cs	495	Active
I don't really have an excuse for not breaking process frame up into even more methods, except the parameter lists are extremely ugly 
already, so I didn't want to have to write even more of those methods. The other 2 warnings are about Level creator and a factory I believe.
They just exist to be giant if statements, so there's not much that can be done about their cyclomatic complexity.

SPRINT 5 README

The menus allow you to select from multiple modes. Singleplayer mode will put you in charge of one Mario, multiplayer allows you to control two Marios using 
WASD and the arrow keys. Multiplayer has a flag at the end of the level that you should use to end the game. We never did fix the fact that you can make it through the
middle of the flag, so just try to avoid jumping at the middle of it, or you'll have to get back through. In singleplayer, there is a boss battle beyond where the flag
would normally be in multiplayer. There aren't really any notable bugs that I can think of this sprint, the biggest issue is just that the sprites are comparatively really
tall, but we don't want to scale them down because they look really nice. So, sometimes if you aren't careful grabbing a powerup you can get stuck with your head in a block,
but it's usually easy to get out. The boss is really hard, so set his hits to one in the BowserObject class.  I think that's actually it for the readme for once.

Ignored code warnings:
OK I'll be honest it's 11:47 I don't have time to list all of these specifically. All of the warnings that appeared had already appeared in some form in earlier sprints.
I tried my best to list a lot of them out though.

Warnings ignored for same reasons as the previous sprints:
-CA1822 : 'this' parameter (or 'Me' in Visual Basic) of 'EnemyFactory.Create(EnemyType, Vector2)'	5 of the errors
-CA1014: Mark 'MarioClone.exe' with CLSCompliant(true) because it exposes externally visible types.		1 of the errors
-CA1001: Implement IDisposable on 'HitBox'		6 of the errors (level creator is never disposed, so we'd never write a method for it)
-CA 1814: Replace gamegrid with jagged array	1 of the errors
-CA2214: 'RedKoopaObject.RedKoopaObject(ISprite, Vector2)' contains a call chain that results in a call to a virtual method defined 
by the class. Review the following call stack for unintended consequences:		45 of the errors
	--this one was discussed with you, and we're just not sure how to fix it, same reason as before

5 of the errors
--Warning	CA1026	Replace method 'Camera.Move(Vector2, bool)' with an overload that supplies all default arguments.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Cam\Camera.cs	90	Active
Several methods (Mostly draws) supply just a few default arguments. It seems silly that we can't (monogame does I believe), because some 
arguments are nearly always one particular value, the extra argument actually only exists to satisfy 1-2 particular classes in a lot of 
cases, while still fitting an abstract class or interface. Additionally, for the Camera one, I think that came with the code you gave us for
the camera.

3 of the errors
Warning	CA1502	'CollisionManager.ProcessFrame(GameTime, List<AbstractGameObject>, GameGrid)' has a cyclomatic complexity of 58. Rewrite or refactor the method to reduce complexity to 25.	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\Collision\CollisionManager.cs	495	Active
I don't really have an excuse for not breaking process frame up into even more methods, except the parameter lists are extremely ugly 
already, so I didn't want to have to write even more of those methods. The other 2 warnings are about Level creator and a factory I believe.
They just exist to be giant if statements, so there's not much that can be done about their cyclomatic complexity.10 of errors

10 of the errors
--Warning	CA1002	Change 'List<AbstractGameObject>' in 'BreakableBrickObject.PieceList' to use Collection<T>, ReadOnlyCollection<T> or KeyedCollection<K,V>	MarioClone	C:\Users\Anna Wolfe\source\repos\General Mills Frosted Flakes\MarioClone\GameObjects\Bricks\BreakableBrickObject.cs	14	Active
These kind of go together. A lot of random things, I believe all of them lists, give warnings because they are lists exposed outside of
their class. For the same reasons as given in the previous sprint, I'm not going to convert all of these to Collections. Also, on some
of the objects, the lists actually need to be public as they're accessed by other methods. If I changed them to a property, it would probably
just give CA1002 instead, gaining me a net reduction of 0 errors.