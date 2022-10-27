#ifndef SUB_PUB_SIMULATED_CAMERA_H
#define SUB_PUB_SIMULATED_CAMERA_H
#include "ICamera.h"
#include "boost/array.hpp"
#include "boost/asio.hpp"
#include "boost/shared_ptr.hpp"
#include "boost/system/error_code.hpp"
#include "boost/system/linux_error.hpp"
#include "boost/system/system_error.hpp"

#define PORT 42000
#define IP_ADDRESS "127.0.0.1"
using tcp = boost::asio::ip::tcp;

class simulated_camera : public ICamera {
public:
    simulated_camera(){

    }

    virtual ~simulated_camera();
//    cv::Mat getImage() override;
    void getData();
private:
//    cv::Mat convertByteArrayToMat(boost::shared_ptr<unsigned char[1024][640]> &);
    boost::shared_ptr<unsigned char[1024][640]> m_byteArr;
    int m_dataSize;
    unsigned char* m_dataArray;
};
#endif// SUB_PUB_SIMULATED_CAMERA_H