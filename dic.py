# import OS module
import os
import json 

 
path = "C:\\Users\\tonmo\\Desktop\\New Feature\\ARMTemplates"
dir_list = os.listdir(path)

dict = {}
 
for dir in dir_list :
    path_dir = os.path.join(path, dir)
    
    if os.path.isfile(path_dir):
        print("This is file")
    else :
        dict[dir] = []
        inner_list = os.listdir(path_dir)
        for inner in inner_list :
            if os.path.isfile(os.path.join(path_dir, inner)):
                dict[dir].append(inner)

with open("sample.json", "w") as outfile:
    json.dump(dict, outfile)