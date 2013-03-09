# Ant properties for building the springapp
tomcat.home=${Technology.Tomcat.Dir}
deploy.path=${"${"}tomcat.home}/webapps
tomcat.manager.url=http://localhost:${Technology.Tomcat.Port}/manager
tomcat.manager.username=${Technology.Tomcat.Username}
tomcat.manager.password=${Technology.Tomcat.Password}
