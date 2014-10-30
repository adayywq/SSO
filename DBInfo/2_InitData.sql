
/* UserInfo(用户信息表) */
Insert into UserInfo (UserID,UniteId,LoginType,LoginId,Name,Email,Mobile,Pwd,InitIdent,DataSource,CreateDate,State) values (-1,0,'sso','admin','超管员','','','D88CBDDA2D11A406D3904720DB6E1DA8','1','sso',sysdate,-1);

/* Application(应用表)、Developer(开发者表)、DevAppR(开发应用关系表) */
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'统一登录','sso',0,1);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'用户登录','login',(AppID.Currval-1),1);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'用户状态','verify',(AppID.Currval-2),2);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'用户信息','getuser',(AppID.Currval-3),3);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'安全退出','logout',(AppID.Currval-4),4);

Insert into Developer (DevId,AccCode,DevName,DevCode,LinkMan,Mobile,Email,SiteUrl,CallbackUrl,LogoutUrl,Memo,Creator,CreatDate,Modifier,ModifyDate,State) values (DevId.Nextval,'','校园一卡通管理平台','GLPT','杨伟强','','yangweiqiang@synjones.net','http://192.168.10.71:8001/','/Main.aspx','#','',-1,sysdate,-1,sysdate,1);
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-3));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-2));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-1));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval));
Insert into Developer (DevId,AccCode,DevName,DevCode,LinkMan,Mobile,Email,SiteUrl,CallbackUrl,LogoutUrl,Memo,Creator,CreatDate,Modifier,ModifyDate,State) values (DevId.Nextval,'','校园一卡通服务平台','FWPT','杨伟强','','','yangweiqiang@synjones.net','/Account/Index.aspx','/Main.aspx','',-1,sysdate,-1,sysdate,1);
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-3));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-2));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-1));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval));

Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'个人消息','message',0,2);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'发送消息','sendmsg',(AppID.Currval-1),1);




