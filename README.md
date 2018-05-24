# Smart-UPS-Tester


Software used for monitoring, service and testing UPS devices. The device has premade communication protocol and two communication ports (USB and RS232), when the link is established commands are sent, after receiving the commands the device sends back voltage and current of different components on the UPS via UART. The app receives the information from the device parse them and puts the values in a datatable. 

Functions: 
Test Napetosti(checks the voltage levels for abnormalities) ,
Test Balancer(turns on and off the balancers and makes sure they work),
Test Breme(checks the current levels for abnormalities),

Services: 
Charging ON/OFF,
Test Battery,
EEPROM Reset,
UPS test mode ON/OFF



