# UnityDataReplay
A system for replaying recorded motions (position and rotation data) in Unity.

To record data from Unity, I've used Alex's UnityDataLogging package (https://github.com/Avdbergnmf/UnityDataLogging).
Alternatively, use my fork which includes a super simple GameObject Logger (https://github.com/pryxe023/UnityDataLogging).

## How it works
The recorded data (see above) is in csv-files. This (Re)player allows you to move GameObjects based on the information in these csv-files.

**This is still a work in progress.**

:construction:

### CSV format
It is important that the format of your csv-files is as follows (from left to right);

1. ~Title~
1. Position X
1. Position Y
1. Position Z
1. Rotation X
1. Rotation Y
1. Rotation Z
1. Rotation W
1. ~Timestamp~
