# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.23

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:

#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:

# Disable VCS-based implicit rules.
% : %,v

# Disable VCS-based implicit rules.
% : RCS/%

# Disable VCS-based implicit rules.
% : RCS/%,v

# Disable VCS-based implicit rules.
% : SCCS/s.%

# Disable VCS-based implicit rules.
% : s.%

.SUFFIXES: .hpux_make_needs_suffix_list

# Command-line flag to silence nested $(MAKE).
$(VERBOSE)MAKESILENT = -s

#Suppress display of executed commands.
$(VERBOSE).SILENT:

# A target that is always out of date.
cmake_force:
.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /snap/clion/209/bin/cmake/linux/bin/cmake

# The command to remove a file.
RM = /snap/clion/209/bin/cmake/linux/bin/cmake -E rm -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub"

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug"

# Include any dependencies generated for this target.
include CMakeFiles/sub_Pub.dir/depend.make
# Include any dependencies generated by the compiler for this target.
include CMakeFiles/sub_Pub.dir/compiler_depend.make

# Include the progress variables for this target.
include CMakeFiles/sub_Pub.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/sub_Pub.dir/flags.make

CMakeFiles/sub_Pub.dir/main.cpp.o: CMakeFiles/sub_Pub.dir/flags.make
CMakeFiles/sub_Pub.dir/main.cpp.o: ../main.cpp
CMakeFiles/sub_Pub.dir/main.cpp.o: CMakeFiles/sub_Pub.dir/compiler_depend.ts
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir="/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/sub_Pub.dir/main.cpp.o"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -MD -MT CMakeFiles/sub_Pub.dir/main.cpp.o -MF CMakeFiles/sub_Pub.dir/main.cpp.o.d -o CMakeFiles/sub_Pub.dir/main.cpp.o -c "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/main.cpp"

CMakeFiles/sub_Pub.dir/main.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/sub_Pub.dir/main.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/main.cpp" > CMakeFiles/sub_Pub.dir/main.cpp.i

CMakeFiles/sub_Pub.dir/main.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/sub_Pub.dir/main.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/main.cpp" -o CMakeFiles/sub_Pub.dir/main.cpp.s

CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o: CMakeFiles/sub_Pub.dir/flags.make
CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o: ../src/subscriber.cpp
CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o: CMakeFiles/sub_Pub.dir/compiler_depend.ts
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir="/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_2) "Building CXX object CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -MD -MT CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o -MF CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o.d -o CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o -c "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/subscriber.cpp"

CMakeFiles/sub_Pub.dir/src/subscriber.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/sub_Pub.dir/src/subscriber.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/subscriber.cpp" > CMakeFiles/sub_Pub.dir/src/subscriber.cpp.i

CMakeFiles/sub_Pub.dir/src/subscriber.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/sub_Pub.dir/src/subscriber.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/subscriber.cpp" -o CMakeFiles/sub_Pub.dir/src/subscriber.cpp.s

CMakeFiles/sub_Pub.dir/src/publisher.cpp.o: CMakeFiles/sub_Pub.dir/flags.make
CMakeFiles/sub_Pub.dir/src/publisher.cpp.o: ../src/publisher.cpp
CMakeFiles/sub_Pub.dir/src/publisher.cpp.o: CMakeFiles/sub_Pub.dir/compiler_depend.ts
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir="/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_3) "Building CXX object CMakeFiles/sub_Pub.dir/src/publisher.cpp.o"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -MD -MT CMakeFiles/sub_Pub.dir/src/publisher.cpp.o -MF CMakeFiles/sub_Pub.dir/src/publisher.cpp.o.d -o CMakeFiles/sub_Pub.dir/src/publisher.cpp.o -c "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/publisher.cpp"

CMakeFiles/sub_Pub.dir/src/publisher.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/sub_Pub.dir/src/publisher.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/publisher.cpp" > CMakeFiles/sub_Pub.dir/src/publisher.cpp.i

CMakeFiles/sub_Pub.dir/src/publisher.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/sub_Pub.dir/src/publisher.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/publisher.cpp" -o CMakeFiles/sub_Pub.dir/src/publisher.cpp.s

CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o: CMakeFiles/sub_Pub.dir/flags.make
CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o: ../src/simulated_camera.cpp
CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o: CMakeFiles/sub_Pub.dir/compiler_depend.ts
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir="/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_4) "Building CXX object CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -MD -MT CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o -MF CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o.d -o CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o -c "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/simulated_camera.cpp"

CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.i"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/simulated_camera.cpp" > CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.i

CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.s"
	/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/src/simulated_camera.cpp" -o CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.s

# Object files for target sub_Pub
sub_Pub_OBJECTS = \
"CMakeFiles/sub_Pub.dir/main.cpp.o" \
"CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o" \
"CMakeFiles/sub_Pub.dir/src/publisher.cpp.o" \
"CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o"

# External object files for target sub_Pub
sub_Pub_EXTERNAL_OBJECTS =

sub_Pub: CMakeFiles/sub_Pub.dir/main.cpp.o
sub_Pub: CMakeFiles/sub_Pub.dir/src/subscriber.cpp.o
sub_Pub: CMakeFiles/sub_Pub.dir/src/publisher.cpp.o
sub_Pub: CMakeFiles/sub_Pub.dir/src/simulated_camera.cpp.o
sub_Pub: CMakeFiles/sub_Pub.dir/build.make
sub_Pub: /usr/lib/x86_64-linux-gnu/libboost_system.so
sub_Pub: CMakeFiles/sub_Pub.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir="/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug/CMakeFiles" --progress-num=$(CMAKE_PROGRESS_5) "Linking CXX executable sub_Pub"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/sub_Pub.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/sub_Pub.dir/build: sub_Pub
.PHONY : CMakeFiles/sub_Pub.dir/build

CMakeFiles/sub_Pub.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/sub_Pub.dir/cmake_clean.cmake
.PHONY : CMakeFiles/sub_Pub.dir/clean

CMakeFiles/sub_Pub.dir/depend:
	cd "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug" && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub" "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub" "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug" "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug" "/home/mhd/UnityProjects/LidarCameraSimUnity/sandboxeProjects/sub&Pub/cmake-build-debug/CMakeFiles/sub_Pub.dir/DependInfo.cmake" --color=$(COLOR)
.PHONY : CMakeFiles/sub_Pub.dir/depend
