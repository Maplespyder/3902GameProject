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

