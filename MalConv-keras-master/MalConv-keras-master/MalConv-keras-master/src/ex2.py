import random

import matplotlib.pyplot as plt
import csv
import random

import pandas as pd

x = []
y = []
Names = []
Values = []
i = 0
df = pd.read_csv(r'C:\Users\FikriSametMert\Desktop\MalConv-keras-master\MalConv-keras-master\MalConv-keras-master\src\samp\result.csv')
shuffled_df = df.sample(frac=1)
aa = df.iloc[981:,2:3]

aa2 = shuffled_df.iloc[:,1:2]
with open('E:\\Studies\\TEZ\\MalConv-keras-master\\MalConv-keras-master\\src\\samp\\result.csv','r') as csvfile:
    plots = csv.reader(csvfile, delimiter = ',')
    for row in plots:
        i+=1
        x.append(row[1])
        y.append((row[2]))
        if i == 500:
            break



plt.hist(aa)
plt.show()

plt.hist(aa2)
plt.show()