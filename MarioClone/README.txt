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



