# UnityDataReplay
A system for replaying recorded motions (position and rotation data) in Unity.

To record data from Unity, I've used Alex's (https://github.com/Avdbergnmf) UnityDataLogging package with my (super simple) GameObject logger found in my fork (https://github.com/pryxe023/UnityDataLogging).

## How it works
The recorded data (see above) is in csv-files. This (Re)player allows you to move GameObjects based on the information in these csv-files.

**This is still a work in progress.**

:construction:

### CSV format
It is important that the format of your csv-files is as follows (from left to right);

1. Title
1. Position X
1. Position Y
1. Position Z
1. Rotation X
1. Rotation Y
1. Rotation Z
1. Rotation W
1. Timestamp

Again, for this the use of the UnityDataLogging package with the GameObject logger (see above) is recommended.

## Use

Simply add the `GameObjectMotionReplay.cs` file to your Unity assets and add it to a `GameObject`.
Now drag the csv-file containing your data to the settings in the Inspector and (if needed) edit the scaling and speed.

That's it!

## Future updates

I am considering adding a central Replay Manager to add to a separate GameObject in which you can set the scaling and replay speed for all GameObject at once.
Possibly you could just link all the GameObjects to a csv-file here.
