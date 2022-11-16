#ifndef SUB_PUB_SIMULATED_LIDAR_H
#define SUB_PUB_SIMULATED_LIDAR_H
//rm
#include <pcl/visualization/cloud_viewer.h>
#include <pcl/point_types.h>
#include <pcl/common/projection_matrix.h>
#include <pcl/console/parse.h>
#include <pcl/io/hdl_grabber.h>
#include <pcl/visualization/point_cloud_color_handlers.h>
//#include "pcl-1.12/pcl/point_cloud.h"
//rm
#include "interface_lidar.hpp"

int static constexpr PORT{42001};
std::string static const IP_ADDRESS{"127.0.0.1"};
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
std::shared_ptr<tcp::socket> sock;
int m_counter;
bool m_connected;
//    tcp::socket *sock;
    };

#endif //SUB_PUB_SIMULATED_LIDAR_H