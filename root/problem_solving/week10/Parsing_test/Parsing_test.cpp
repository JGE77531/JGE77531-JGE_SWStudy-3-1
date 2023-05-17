#include <stdio.h>
#include <stdlib.h>

struct input_format {
    char* name;
    int score;
    char grade;
};

int main() {
    struct input_format inputs[100];
    int count = 0;

    FILE* fp;
    char buffer[100];
    if (fopen_s(&fp, "input.txt", "r") != 0) {
        printf("파일 열기 실패함!");
        return 1;
    }
    while (fgets(buffer, 100, fp) != NULL) {
        char* name = (char*)malloc(sizeof(char) * 100);
        int score;
        char grade;
        sscanf_s(buffer, "내 이름은 %[^,], 이번학기 점수는 %d점이고 성적은 %c를 받았습니다.", name, 100, &score, &grade);

        inputs[count].name = name;
        inputs[count].score = score;
        inputs[count].grade = grade;
        count++;
    }
    fclose(fp);

    fp = NULL;
    if (fopen_s(&fp, "output.txt", "w") != 0) {
        printf("파일 열기 실패함!");
        return 1;
    }
    for (int i = 0; i < count; i++) {
        fprintf(fp, "%s\n%d\n%c\n", inputs[i].name, inputs[i].score, inputs[i].grade);
        free(inputs[i].name);
    }
    fclose(fp);

    for (int i = 0; i < count; i++) {
        printf("[%d번째 입력 줄]\nname : %s\nscore : %d\ngrade : %c\n\n", i + 1, inputs[i].name, inputs[i].score, inputs[i].grade);
    }

    return 0;
}