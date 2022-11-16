//
// Created by holgar on 29.10.22.
//

#ifndef SUB_PUB_ILIDAR_H
#define SUB_PUB_ILIDAR_H
#include "boost/shared_ptr.hpp"
#include "boost/asio.hpp"
#include "boost/array.hpp"
#include "boost/system/error_code.hpp"
#include "boost/system/linux_error.hpp"
#include "boost/system/system_error.hpp"


//#include <pcl-1.8/pcl/visualization/cloud_viewer.h>
//#include <pcl-1.12/pcl/visualization/cloud_viewer.h>
#include <pcl/point_types.h>
#include <pcl/common/projection_matrix.h>
#include <pcl/console/parse.h>
#include <pcl/io/hdl_grabber.h>
#include <pcl/visualization/point_cloud_color_handlers.h>
#include <utility>


class ILidar{
public:
    /**
         * Default virtual destructor, necessary for all derived classes
         */
    virtual ~ILidar() = default;

    /**
     * Constructor for rule of five
     */
    ILidar() = default;

    /**
     * Copy Constructor for rule of five
     * @param other object to copy
     */
    ILidar(ILidar const &other) = default;

    /**
     * Move Constructor for rule of five
     * @param other object to move
     */
    ILidar(ILidar &&other) noexcept = default;

    /**
     * Copy operator for rule of five
     * @param other object to copy
     * @return A reference to this object
     */
    auto operator=(ILidar const &other) -> ILidar & = default;

    /**
     * Move operator for rule of five
     * @param other object to move
     * @return A reference to this object
     */
    auto operator=(ILidar &&other) noexcept -> ILidar & = default;

    virtual auto get_PointCloud() -> pcl::PointCloud<pcl::PointXYZI>::Ptr = 0;

};

#endif //SUB_PUB_ILIDAR_H
