//
// Created by mhd on 27.10.22.
//
//UnaryFunction
#include "include/ICamera.h"
#include "include/simulated_camera.h"
#include "chrono"
#include <bitset>
using namespace std;
using namespace cv;
int main() {
    int k=0;
    std::chrono::steady_clock::time_point begin = std::chrono::steady_clock::now();
//    while (k<1000){
    simulated_camera cam;
    cv::Mat img = cam.getImage();
    String windowName = "Sim"; //Name of the window
    namedWindow(windowName); // Create a window
    imshow(windowName, img); // Show our image inside the created window.
    waitKey(0); // Wait for any keystroke in the window
    destroyWindow(windowName);
    k++;
//    }
    std::chrono::steady_clock::time_point end = std::chrono::steady_clock::now();
    std::cout << "Time difference = " << std::chrono::duration_cast<std::chrono::seconds>(end - begin).count() << " [s]" << std::endl;
    return 0;
}