////
//// Created by Mohamad on 10/25/2022.
////
//
//#ifndef SUB_PUB_SUBSCRIBER_H
//#define SUB_PUB_SUBSCRIBER_H
//
//#include "boost/pointer_cast.hpp"
//#include "boost/shared_ptr.hpp"
//#include "boost/asio.hpp"
//#include <memory>
//#include <cstddef>
//
//class subscriber {
//public:
//    subscriber(const std::string& host, const uint16_t port, const std::function<void>& callback);
//    virtual ~subscriber();
//
//private:
//    void Connect( const std::function<void(unsigned char[])>& callback);
//    void Read();
//    std::string _host;
//    uint16_t _port;
//
//    boost::asio::ip::tcp::socket _socket;
//    std::future<void> _result;
//    //Message messagetype
//    //unsigned char is byte here
//    std::function<void(unsigned char[])> _callback;
//    int32_t _token;
//};
//
//
//#endif //SUB_PUB_SUBSCRIBER_H
