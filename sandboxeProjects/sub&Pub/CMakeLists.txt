cmake_minimum_required(VERSION 3.23)
project(sub_Pub)

set(CMAKE_CXX_STANDARD 14)
find_package(OpenCv REQUIRED)
set (Boost_USE_STATIC_LIBS OFF)
find_package (Boost REQUIRED COMPONENTS)
include_directories (${Boost_INCLUDE_DIRS})
include_directories(${OpenCv_INCLUDE_DIRS})

add_executable(sub_Pub main.cpp src/subscriber.cpp include/subscriber.h src/publisher.cpp include/publisher.h src/simulated_camera.cpp include/ICamera.h include/simulated_camera.h)
target_link_libraries(sub_Pub ${OpenCV_LIBS})
target_link_libraries (sub_Pub ${Boost_LIBRARIES})