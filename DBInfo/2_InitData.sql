
/* UserInfo(�û���Ϣ��) */
Insert into UserInfo (UserID,UniteId,LoginType,LoginId,Name,Email,Mobile,Pwd,InitIdent,DataSource,CreateDate,State) values (-1,0,'sso','admin','����Ա','','','D88CBDDA2D11A406D3904720DB6E1DA8','1','sso',sysdate,-1);

/* Application(Ӧ�ñ�)��Developer(�����߱�)��DevAppR(����Ӧ�ù�ϵ��) */
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'ͳһ��¼','sso',0,1);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'�û���¼','login',(AppID.Currval-1),1);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'�û�״̬','verify',(AppID.Currval-2),2);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'�û���Ϣ','getuser',(AppID.Currval-3),3);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'��ȫ�˳�','logout',(AppID.Currval-4),4);

Insert into Developer (DevId,AccCode,DevName,DevCode,LinkMan,Mobile,Email,SiteUrl,CallbackUrl,LogoutUrl,Memo,Creator,CreatDate,Modifier,ModifyDate,State) values (DevId.Nextval,'','������һ��ͨϵͳ','XZXYKTXT','','','','http://192.168.10.69:8080/','#','#','',-1,sysdate,-1,sysdate,1);
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-3));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-2));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-1));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval));
Insert into Developer (DevId,AccCode,DevName,DevCode,LinkMan,Mobile,Email,SiteUrl,CallbackUrl,LogoutUrl,Memo,Creator,CreatDate,Modifier,ModifyDate,State) values (DevId.Nextval,'','�������շ�ϵͳ','XZXSFXT','','','','http://192.168.10.68:86/','#','#','',-1,sysdate,-1,sysdate,1);
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-3));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-2));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval-1));
Insert into DevAppR (DaID,DevID,AppID) values (DaID.Nextval,DevID.Currval,(AppID.Currval));

Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'������Ϣ','message',0,2);
Insert into Application (AppID,AppName,AppCode,PID,OrderNum) values (AppID.Nextval,'������Ϣ','sendmsg',(AppID.Currval-1),1);




