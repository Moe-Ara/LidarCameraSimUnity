//
// Created by Mohamad on 10/25/2022.
//
#include "../include/simulated_camera.h"
#include "iostream"

void simulated_camera::getData() {

    try{
        tcp::endpoint ep(boost::asio::ip::address::from_string(IP_ADDRESS), PORT);
        boost::asio::io_service ioService;
        tcp::socket sock(ioService, ep.protocol());
        sock.connect(ep);
        boost::array<unsigned char ,4> datSize;
        boost::system::error_code err;
        int i=0;
        boost::asio::read(sock,boost::asio::buffer(datSize),err);
        m_dataSize=datSize[0];
        std::cout<< "Data Size: " << m_dataSize <<std::endl;
        char newarray[this->m_dataSize];
        boost::asio::read(sock,boost::asio::buffer(newarray,m_dataSize),err);
        std::cout<<"Data is : ";
//        while (i<m_dataSize){
//            std::cout<<m_dataArray[i];
//            i++;
//        }
        std::cout.write(newarray, m_dataSize );
        sock.close();
    }catch (std::system_error &e){
        e.what();
    }

}
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
simulated_camera::~simulated_camera() noexcept {
    delete m_dataArray;
}