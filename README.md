
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