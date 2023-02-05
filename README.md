
![image](https://dimg04.c-ctrip.com/images/0v56v12000algc1lzDF79.png)

如上图，右键菜单多了几个我自定义的菜单
- 复制文件路径
- 复制文件夹路径
- 我的工具箱 <走配置文件动态创建子菜单，下面会讲>

我上图是在win10操作系统下演示的，在win11系统也测试可用。




csv2xlsx的源码地址： [https://github.com/yuzd/Exporter/tree/master/ConsoleApp](https://github.com/yuzd/Exporter/tree/master/ConsoleApp)

是基于我开源的各种类型转化封装库，比如csv，xlsx，json，list相互转化

开源地址：[https://github.com/yuzd/Exporter](https://github.com/yuzd/Exporter)



有人可能会问 ，如果电脑安装了office的话 直接csv就可以打开为excel啊，
但是默认的双击csv以excel方式打开，对于是大数字会显示成下面这样

![image](https://dimg04.c-ctrip.com/images/0v51x12000alfybqiE802.png)

所以我想要让csv的内容全部以字符串类型展示(就是上面csv3xlsx的功能了)

![image](https://dimg04.c-ctrip.com/images/0v52512000alfzl2cAAF7.png)



<我的工具箱>是动态创建二级子菜单的容器，需要配置下文件

#### 打开menu.txt配置文件
总共要配置4个东西（按照-_-隔开）
- [1]针对什么后缀的文件进行 比如.csv代表只针对csv文件才会创建该菜单
- [2]菜单名称
- [3]这个菜单功能对应的可执行文件 这个根据功能自己开发了
- [4]传给上面可执行文件的参数 {path} 会动态替换为选择的文件地址全路径

**注意，针对[1] 有2个特殊约定**

如果配置 * 那么选择文件或者文件夹都会出现该菜单
```bash
*-_-测试-_-E:\xxxxx\xxxx.exe-_-"{path}"
```
如果配置 folder 那么只会选择文件夹才会出现该菜单
```bash
folder-_-测试-_-E:\xxxxx\xxxx.exe-_-"{path}"
```


这样一来的话，如果我后面想要动态创建菜单，只需要开发这个菜单功能的可执行文件，然后再menu.txt新增个配置就搞定了，
是不是很方便



上面说到下载包有个csv2xlsx.exe文件，下面我们配置下 动态新增一个二级子菜单<转成excel> 只针对csv文件有效

menu.txt配置如下：
```bash

.csv-_-转成excel-_-E:\Tool\windows-extention\csv2xlsx.exe-_-"{path}"


```

我们这么配置下：

![image](https://dimg04.c-ctrip.com/images/0v54612000alg73c4ED56.png)


配置好后，在csv类型文件点击右键，就可以看到效果了
![image](https://dimg04.c-ctrip.com/images/0v55y12000algaq5kD944.png)


点击该菜单，会启动一个cmd窗口
注意，在非csv的文件是不会有这个菜单的，这个是根据配置来的


![image](https://dimg04.c-ctrip.com/images/0v56n12000algb1na825D.png)

其实就是将选中的文件路径传给你配置的可执行文件去执行