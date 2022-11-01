//
// Created by Mohamad on 10/25/2022.
//
#include "../include/simulated_camera.h"
#include "iostream"
#include "bitset"

simulated_camera::simulated_camera() {
    tcp::endpoint ep(boost::asio::ip::address::from_string(IP_ADDRESS), PORT);
    boost::asio::io_service ioService;
    sock = new tcp::socket(ioService, ep.protocol());
    sock->connect(ep);
}

//void simulated_camera::getData() {
//
//    try{
//        boost::array<unsigned char ,4> datSize;
//        boost::system::error_code err;
//        //get data size as a byte array
//        boost::asio::read(*sock,boost::asio::buffer(datSize),err);
//        //convert bytes to int
//        std::memcpy(&m_dataSize,&datSize,sizeof(int));
//        //array to receive data
//        unsigned char m_dataArray[m_dataSize];
//        // read data
//        boost::asio::read(*sock,boost::asio::buffer(m_dataArray,m_dataSize),err);
//        //init pixels
//        int m_pixels[PIC_WIDTH*PIC_HEIGHT];
//        //create pixels and insert them in our vector
//        for (int i =0,k=0; i<m_dataSize,k<PIC_WIDTH*PIC_HEIGHT;i=i+3,k++){
//            //dat = r + g + b
//            unsigned int pixel=0x00000000;                 //0x1211FF00
//            unsigned char r=m_dataArray[i+0];   //0x000000012
//            unsigned char g=m_dataArray[i+1];   //0x000000011
//            unsigned char b=m_dataArray[i+2];   //0x0000000FF
//            pixel |= (r <<16);
//            pixel |= (g <<8);
//            pixel |= (b);
//            //push pixels into this vector
//            m_pixels[k]=pixel;
//        }
//        //Converting Image
//        cv::Mat A( PIC_HEIGHT,PIC_WIDTH,CV_8UC4,m_pixels);
//
//    }catch (std::system_error &e){
//        e.what();
//    }
//
//}
cv::Mat simulated_camera::getImage() {
    try {
        //data size in an array of 4 bytes
        boost::array<unsigned char, 4> datSize;
        //error code from boost library
        boost::system::error_code err;
        //get data size as a byte array
        boost::asio::read(*sock, boost::asio::buffer(datSize), err);
        //convert datasize to int
        std::memcpy(&m_dataSize, &datSize, sizeof(int));
        //array to receive data
        unsigned char data[m_dataSize];
        // read data
        boost::asio::read(*sock, boost::asio::buffer(data, m_dataSize), err);
        //array of pixels
        std::vector<signed int> pixels;
        //create pixels and insert them in an array
        for (int i = 0; i < m_dataSize; i = i + 3) {
            int pixel = 0x00000000;
            unsigned char r = data[i + 0];    //0x000000012
            unsigned char g = data[i + 1];    //0x000000011
            unsigned char b = data[i + 2];    //0x0000000FF
            pixel |= (r << 16);
            pixel |= (g << 8);
            pixel |= (b);
            //push pixels into this vector
            pixels.push_back(pixel);
        }
        //Converting Image
        cv::Mat Image(PIC_HEIGHT, PIC_WIDTH, CV_8UC4 );
        memcpy(Image.data, pixels.data(),pixels.size()*sizeof(int));
        return Image;
    } catch (std::system_error &e) {
        e.what();
    }
}

simulated_camera::~simulated_camera() noexcept {
    sock->close();
    delete sock;
}