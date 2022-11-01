#ifndef SUB_PUB_ICAMERA_H
#define SUB_PUB_ICAMERA_H
#include "opencv2/core.hpp"
#include "boost/shared_ptr.hpp"
#include "boost/asio.hpp"
#include "boost/array.hpp"
class ICamera{
public:
    ICamera(){}
    virtual ~ICamera(){}
   virtual auto getImage()->cv::Mat=0;
};
#endif