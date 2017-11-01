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
