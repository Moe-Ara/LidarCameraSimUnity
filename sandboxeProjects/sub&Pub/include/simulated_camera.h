//
// Created by Mohamad on 10/26/2022.
//

#ifndef SUB_PUB_SIMULATED_CAMERA_H
#define SUB_PUB_SIMULATED_CAMERA_H
#include "ICamera.h"
class simulated_camera:public ICamera
{
public:
    simulated_camera();
    virtual ~simulated_camera();
    cv::Mat getImage() override;
private:
    cv::Mat convertByteArrayToMat(boost::shared_ptr<unsigned char[1024][640]>&);
    boost::shared_ptr<unsigned char[1024][640]> m_byteArr;
};
#endif //SUB_PUB_SIMULATED_CAMERA_H
