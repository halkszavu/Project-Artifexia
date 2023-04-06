# Project-Artifexia
A rotation file editor for GPlates

This Editor requires GPlates which is a free plate tectonic simulator. You can download it from [here](https://www.gplates.org/download).

## Basic features 

* Opening and saving .rot files, which are read by GPlates for reconstruction of plate movements.
* Basic manipulation of the file in tandem with GPlates such as
  * Creating new plate, with its plate ID and timestamps
  * Joining up existing plates at a certain timestamp
  * Initiating independent movement of two co-moving plates
  * Creating the drift correction, so the plates will not move back to their original position after the last existing timestamp
  <sup>This drift correction inserts a new line/updates the line at 1.0 Ma.<sup>
* Creating a new .rot file. (planned)
  
## Extra features (planned or present)

* A rudimentary help on how to handle the main manipulations
* Naming the plates and making a system to read/write these names into the .rot file. These names are used for the other comments, and are displayed in the plate lists (alongside the plateIDs)
