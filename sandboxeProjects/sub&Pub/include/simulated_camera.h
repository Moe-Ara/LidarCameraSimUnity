#ifndef SUB_PUB_SIMULATED_CAMERA_H
#define SUB_PUB_SIMULATED_CAMERA_H
#include "ICamera.h"
#include "boost/array.hpp"
#include "boost/asio.hpp"
#include "boost/shared_ptr.hpp"
#include "boost/system/error_code.hpp"
#include "boost/system/linux_error.hpp"
#include "boost/system/system_error.hpp"
#include <functional>
#include "opencv2/opencv.hpp"

//#define PORT 42000
//#define IP_ADDRESS "127.0.0.1"
#define PIC_WIDTH 1024
#define PIC_HEIGHT 640

using tcp = boost::asio::ip::tcp;

class simulated_camera : public ICamera {
public:
    simulated_camera();
    virtual ~simulated_camera();
    cv::Mat getImage() override;
private:
    int m_dataSize;
    tcp::socket *sock;
};
#endif// SUB_PUB_SIMULATED_CAMERA_H