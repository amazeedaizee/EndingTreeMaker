
# EndingTreeMaker

![image](https://github.com/amazeedaizee/EndingTreeMaker/assets/131136866/4a599794-0bf8-4ffb-b45b-b09f18d996ac)

A program that creates and organizes ending runs for Needy Streamer Overload \(**Windows** only\).
Before using this program, you should have a moderate understanding of some of the core mechanics from the game already.

**You also must have Needy Streamer Overload (1.1.0+) installed through Steam in order for this program to work properly.**
Pirated or past copies of the game might not work well with this program.

**\*Stats calculated is based on the Steam version**

## What is an Ending Tree?

Basically a nickname for what I call a **full playthrough of the game on one save**, but for completing endings.

Every time the player gets an ending from the game, if they want to complete another ending, they would have to reload on a specific day, since getting an ending brings them back to the login screen. Some ending runs can start on that same day, but the endings that each run gets are different based on the players actions, or, that day or past days "branching" out into different ending scenarios. (so basically I like to call **individual ending runs associated with an ending tree "branches"**).

This program helps with planning and organizing said branches into a single ending tree.

This was mostly made with speed-runners in mind (since the speed-running category that involves getting the true ending requires all endings to be done on a single save file), however anyone can use this if they want.

## Features

- Easy way of organizing branches into ending trees
- Saves and loads ending trees using JSON files
- Can export ending trees into a CSV file
- Accurately calculates stat changes from doing actions like in the game
- Can predict endings projected to happen (if applicable) in a branch
- ...and some other things I might be missing.

## Some notes

The program has a check for branches to see if the ending run can also be done the same way in the game. So while it is possible to choose the stream action at noon when editing branches, it would not be possible to save the branch since streaming can only be done at night in the game.

Event flags when activated during branch editing might force you to do specific actions in said branch, if the day the event flag was set is not less than the starting day of the branch.

Non-stream Darkness actions have their own parent action category.

Ame's Parents and Music Video action events are found in the Go Out parent category. 

Branches and ending trees in this program also do not take into account stat changes from random noon events \(ESPECIALLY the Mentally Healthy noon events\), or removing stressful comments. When creating branches and ending trees, its also good to take into account the possibility of these events, as your branch might end in a different ending than you expect. 

With that being said, **Blazing Hell** (which can be achieved from a random noon event while Darkness is 60+) isn't available as one of the endings you can choose when making or editing a branch, nor the program will predict if Blazing Hell is an expected ending. This program only focuses on endings that don't rely on RNG to get. If you're aiming for a 100% endings run, you should take into account Blazing Hell in relation to other branches when planning out your ending tree.

**Also this is actually the first program I wrote, so some stuff might not be completely perfect. (that's why this program is only available on Windows.) If something doesn't work as intended, I apologize in advance!**

If you have any issues, questions or concerns about the program, you can open up an issue on the Issues page.

**Is this program being flagged as a virus?** See [HERE](https://github.com/amazeedaizee/EndingTreeMaker/issues/1) for more details.

## Other

This program is fan-made and is not affiliated with xemono or WSS Playground.  All properties belong to their respective owners.

Haven't downloaded Needy Streamer Overload yet? Get it here: https://store.steampowered.com/app/1451940/NEEDY_STREAMER_OVERLOAD/
