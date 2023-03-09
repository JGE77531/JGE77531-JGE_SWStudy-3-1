#include <iostream>

using namespace std;

int main()
{
    unsigned short count;
    
    std::cin>>count;
    
    while(count != 0){
        cout << count%10;
        count /= 10;
    }
}