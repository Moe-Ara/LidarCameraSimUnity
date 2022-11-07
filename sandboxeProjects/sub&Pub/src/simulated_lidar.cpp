//
// Created by holgar on 29.10.22.
//

#include "../include/simulated_lidar.hpp"
#include "iostream"
//using namespace std;
simulated_lidar::simulated_lidar() {    tcp::endpoint ep(boost::asio::ip::address::from_string(IP_ADDRESS), PORT);
    boost::asio::io_service ioService;
    sock=std::make_unique<tcp::socket>(ioService, ep.protocol());
//    sock = new tcp::socket(ioService, ep.protocol());

    sock->connect(ep);}

auto simulated_lidar::get_PointCloud() -> pcl::PointCloud<pcl::PointXYZI>::Ptr {

    pcl::PointCloud<pcl::PointXYZI>::Ptr pointCloud (new pcl::PointCloud<pcl::PointXYZI>);

    try{
        boost::array<unsigned char, 4> datSize;
        boost::system::error_code err;
        boost::asio::read(*sock,boost::asio::buffer(datSize),err);
//        std::memcpy(&m_dataSize, &datSize, sizeof(int));
        unsigned int m_dataSize= (datSize[3] << 24) | (datSize[2] << 16) | (datSize[1] << 8) | (datSize[0]);
        auto data = std::make_shared<std::vector<unsigned char>>(m_dataSize);
//        unsigned char *m_dataArray;
//        m_dataArray= new unsigned char [m_dataSize];
        boost::asio::read(*sock, boost::asio::buffer(*data, m_dataSize), err);

        int i;
        int j;
        unsigned int data_point;
        for (i = 0; i < m_dataSize; i = i + 12){

            float point[3];
            int cor = 0;
            for (j = i; j < i + 12; j = j + 4) {
                data_point = (data->at(j + 3) << 24) | (data->at(j + 2) << 16) | (data->at(j + 1) << 8) |
                             (data->at(j));
//                data_point = (m_dataArray[j + 3] << 24) | (m_dataArray[j+2] << 16) | (m_dataArray[j+1] << 8) |
//                             (m_dataArray[j]);

                float to_float;
                std::memcpy(&to_float, &data_point, 4);
                point[cor] = to_float;
                cor++;
            }
            pcl::PointXYZI pcl_point(1.0f);
            pcl_point.x = point[0];
            pcl_point.y = point[1];
            pcl_point.z = point[2];
            pointCloud->push_back(pcl_point);

        }
//        delete [] m_dataArray;
    }catch(std::system_error &e){
        e.what();
    }
//    std::cout<<"returning ";
    return pointCloud;

}
//
//pcl::PointCloud<pcl::PointXYZI>::Ptr simulated_lidar::getData() {
//
//    try{
//
//        boost::array<unsigned char, 4> datSize;
//        boost::system::error_code err;
//        int i;
//        boost::asio::read(sock,boost::asio::buffer(datSize),err);
//        unsigned int m_dataSize= (datSize[3] << 24) | (datSize[2] << 16) | (datSize[1] << 8) | (datSize[0]);
//        //m_dataSize = [0] + [1] + datSize[2] + datSize[3];
//        std::cout<< "Data Size: " << m_dataSize <<std::endl;
//        //char newarray[this->m_dataSize];
//        m_dataArray = new unsigned char [m_dataSize];
//        boost::asio::read(sock,boost::asio::buffer(m_dataArray,m_dataSize),err);
//        int j;
//        unsigned int data_point;
//        std::cout << "Size of float: " << sizeof(float) << std::endl;
//        std::cout << "/4: " << m_dataSize / 4 << std::endl;
//        std::cout << "/12: " << m_dataSize / 12 << std::endl;
////        for (j = 0; j < m_dataSize; j++{
////
////        }
//        //pcl-1.8/
//        //pcl::PointCloud<pcl::PointXYZI> pointCloud(1, 1, 0.0f);
//        pcl::PointCloud<pcl::PointXYZI>::Ptr pointCloud (new pcl::PointCloud<pcl::PointXYZI>);
//        for (i = 0; i < m_dataSize; i = i + 12){
//            float point[3];
//            int cor = 0;
//            for (j = i; j < i + 12; j = j + 4) {
////          for (j = 0; j < m_dataSize; j = j + 4) {
//                //std::cout << "[";
//                //           for (k = j + 3; k >= j; k--) {
//                //               std::cout << std::hex <<  m_dataArray[k] + 0;
//                //
//                //           }
//                //std::cout << std::endl;
//                data_point = (m_dataArray[j + 3] << 24) | (m_dataArray[j + 2] << 16) | (m_dataArray[j + 1] << 8) |
//                             (m_dataArray[j]);
//
//                //if (j > 128) {
//                //   break;
//                // }
//                float to_float;
//                std::memcpy(&to_float, &data_point, 4);
//                //std::cout << "float: " << to_float << std::endl;
//                point[cor] = to_float;
//                cor++;
//            }
//            //std::cout << "point: " << point[0] << ", " << point[1] << ", " << point[2] << std::endl;
//            pcl::PointXYZI pcl_point(1.0f);
//            pcl_point.x = point[0];
//            pcl_point.y = point[1];
//            pcl_point.z = point[2];
//            std::cout <<"POINTXYZI: " << pcl_point.x << ", " << pcl_point.y << ", " << pcl_point.z << ", "  << std::endl;
//            pointCloud->push_back(pcl_point);
//
//
//        }
//        //pointCloud.height = 16;
//        std::cout << pointCloud;
//        //pointCloud.pcl::visualization::CloudViewer("test") =;
//        //pcl::visualization::CloudViewer::GrayCloud testy = pointCloud;
//        //testy.pcl::visualization::CloudViewer::showCloud()
//
//        //pcl::PointCloud<pcl::PointXYZRGB>::Ptr cloud;
//        //... populate cloud
//        //pcl::PointCloud<pcl::PointXYZI>::Ptr cloud = &pointCloud;
//
//
//
//
//        //std::cout.write(m_dataArray, m_dataSize );
//        sock.close();
//        return pointCloud;
//
//    }catch (std::system_error &e){
//        e.what();
//    }
//
//}
//cv::Mat simulated_camera::getImage() {
//    //TODO:IMPLEMENT THIS METHOD
//
//
//    return convertByteArrayToMat(m_byteArr);
//}

//cv::Mat simulated_camera::convertByteArrayToMat(boost::shared_ptr<unsigned char[1024][640]> &dat) {
//    cv::Mat pic(1024,640,CV_64F);
//    std::memcpy(pic.data,&dat, 1024*640*sizeof(char));
//    return pic;
//}
//simulated_lidar::~simulated_lidar() noexcept {
//    //pcl::visualization::~CloudViewer();
////    delete m_dataArray;
//sock->close();
//}
simulated_lidar::~simulated_lidar() {
    //close socket
    sock->close();
//    delete sock;
    //delete socket
//    delete sock;
}