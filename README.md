# 基于.NET6的webApi脚手架项目

- 控制器，服务，仓储三层模板
- 集成可用的基于JWT的授权认证的简单的用户服务（具体可各个项目扩展）
- 集成Hangfire.Httpjob的支持
- 支持.NET命令行的模板安装和新建

## 模板的使用

先CD到项目根目录（.template.config文件夹所在的目录），然后执行下面命令安装模板

> dotnet new install .\

根据模板创建新项目

> dotnet new Mango.Scaffold.Project -n '需要替换的项目名称'

执行后会把项目中的Scaffold都替换为自定义的名称。