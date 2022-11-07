#ifndef SUB_PUB_SIMULATED_LIDAR_H
#define SUB_PUB_SIMULATED_LIDAR_H
//rm
#include <pcl-1.12/pcl/visualization/cloud_viewer.h>
#include <pcl-1.12/pcl/point_types.h>
#include <pcl-1.12/pcl/common/projection_matrix.h>
#include <pcl-1.12/pcl/console/parse.h>
#include <pcl-1.12/pcl/io/hdl_grabber.h>
#include <pcl-1.12/pcl/visualization/point_cloud_color_handlers.h>
//#include "pcl-1.12/pcl/point_cloud.h"
//rm
#include "interface_lidar.hpp"

#define PORT 42001
#define IP_ADDRESS "127.0.0.1"
using tcp = boost::asio::ip::tcp;

class Ptr;

class simulated_lidar : public ILidar {
public:
    simulated_lidar();
    ~simulated_lidar() override;
    auto get_PointCloud() -> pcl::PointCloud<pcl::PointXYZI>::Ptr override;

private:
//    int m_dataSize;
//    unsigned char *m_dataArray;
std::unique_ptr<tcp::socket> sock;
//    tcp::socket *sock;
    };

#endif //SUB_PUB_SIMULATED_LIDAR_H