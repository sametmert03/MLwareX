import os
import csv
import datetime

path = "E:\\Studies\\TEZ\\MalConv-keras-master\\MalConv-keras-master\\src\\\Dataset\\Dataset\\Benigns\\"
dir_list = os.listdir(path)


print(dir_list)

fruits = []
dates = []

for i in dir_list:
    hour_and_minute = datetime.datetime.now().strftime("%c")
    i = path + i
    a = list(i.split(" "))
    a.append(1)
    a.append(hour_and_minute)
    print(a)
    fruits.append(a)


print("######################################")
print(fruits)



with open('countries.csv', 'a', encoding='UTF8', newline='') as f:
    writer = csv.writer(f)
    writer.writerows(fruits)