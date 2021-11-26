# UnityDataReplay
A system for replaying recorded motions (position and rotation data) in Unity.

This script uses data recorded and saved using my UnityToMatlabUDP project (https://github.com/pryxe023/UnityToMatlabUDP).

## How it works
The recorded data (see above) is in csv-files. This (Re)player allows you to move GameObjects based on the information in these csv-files.

### CSV format
It is important that the format of your csv-files is as follows (from left to right);

1. Position X
1. Position Y
1. Position Z
1. Rotation X
1. Rotation Y
1. Rotation Z
1. Timestamp
1. Scale of recording (useful when playing on a scaled object)

## Use

Simply add the `GameObjectMotionReplay.cs` and `ReplayManager.cs` files to your Unity assets.

Create `GameObject`s to be controlled by the scripts. Attach the `GameObjectMotionReplay.cs` to each `GameObject`.
In this script, attach the csv-file containing the positional and rotational data to the correct `GameObject`.

Next, create an empty `GameObject` (and for simplicity call it 'Replay Manager') and attach the `ReplayManager.cs` script to it.
Here you can change general settings and connect the replayable instances of `GameObject` to the manager.

That's it!

## Future updates

Nothing in the works right now, but I might edit the script to find the correct values (position X, rotation X, etc. etc.) based on the headers in the csv-file automatically. This isn't difficult to implement, but not necessary for my needs currently. However, feel free to fork this project and implement it (or send me a message if you really want me to implement it).

### Known issues

* Unity seems to be unable (sometimes) to play the replay fast enough to keep up with the actual time. May need to skip some timesteps to fix this.
