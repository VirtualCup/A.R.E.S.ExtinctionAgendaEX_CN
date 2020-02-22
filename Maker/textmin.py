from StringHelper import StringHelper
import os
import sys

reload(sys) 
sys.setdefaultencoding('utf8')

def get_file_list(dir, list):
	if os.path.isfile(dir):
		list.append(dir)
	elif os.path.isdir(dir):
		for sub in os.listdir(dir):
			new_dir = os.path.join(dir, sub)
			get_file_list(new_dir, list)
	return list;

sh = StringHelper();
for f in get_file_list("Code", []):
	sh.add_file_text(f);
sh.add_western();

str = sh.get_chars();
f = open("Font\lang.txt", "wb");
f.write(str);
f.close();