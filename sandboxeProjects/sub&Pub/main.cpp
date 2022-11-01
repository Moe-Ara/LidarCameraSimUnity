//
// Created by mhd on 27.10.22.
//
//UnaryFunction
#include "include/ICamera.h"
#include "include/simulated_camera.h"

#include <bitset>
using namespace std;
using namespace cv;
int main() {
    int k=0;
//    while (k<100){
    simulated_camera cam;
    cv::Mat img = cam.getImage();
    String windowName = "The Guitar"; //Name of the window
    namedWindow(windowName); // Create a window
    imshow(windowName, img); // Show our image inside the created window.
    waitKey(0); // Wait for any keystroke in the window
    destroyWindow(windowName);
    k++;
//    }
    return 0;
}