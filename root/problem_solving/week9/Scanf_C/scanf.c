#include <stdio.h>
#include <string.h>

void S_canf(char *str, char *typ);

int main()
{
    char str[100], typ[100];
    fgets(str, 100, stdin);
    str[strcspn(str, "\n")] = 0; // 개행문자 제거
    fgets(typ, 100, stdin);
    typ[strcspn(typ, "\n")] = 0; // 개행문자 제거
    S_canf(str, typ);
    return 0;
}

void S_canf(char *str, char *typ)
{
    char *ptr, *type;
    int num;

    ptr = strtok(str, " \t");
    type = strtok(typ, " \t");
    
    while (ptr != NULL && type != NULL) {
        if (strcmp(type, "%d") == 0) {
            num = atoi(ptr);
            printf("%d ", num);
        } else {
            printf("%s ", ptr);
        }
        ptr = strtok(NULL, " \t");
        type = strtok(NULL, " \t");
    }
    printf("\n");
}