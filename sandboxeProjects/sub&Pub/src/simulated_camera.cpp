//
// Created by Mohamad on 10/25/2022.
//
#include "../include/simulated_camera.h"

simulated_camera::simulated_camera() {
    boost::shared_ptr<unsigned char[1024][640]> m_byteArr;
}

cv::Mat simulated_camera::getImage() {
    //TODO:IMPLEMENT THIS METHOD
    return convertByteArrayToMat(m_byteArr);
}

cv::Mat simulated_camera::convertByteArrayToMat(boost::shared_ptr<unsigned char[1024][640]> &dat) {
    cv::Mat pic(1024,640,CV_64F);
    std::memcpy(pic.data,&dat, 1024*640*sizeof(char));
    return pic;
}

simulated_camera::~simulated_camera() noexcept {
}