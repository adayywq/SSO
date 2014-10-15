
/*==============================================================*/
/* Table: UserInfo                                              */
/*==============================================================*/
create table UserInfo 
(
   UserID             INT                  not null,
   UniteID            INT,
   LoginType          VARCHAR(20),
   LoginID            VARCHAR(20),
   Name               VARCHAR(20),
   Email              VARCHAR(200),
   Mobile             VARCHAR(20),
   Pwd                VARCHAR(200),
   InitIdent          VARCHAR(10),
   DataSource         VARCHAR(50),
   CreateDate         DATE,
   State              INT,
   constraint PK_USERINFO primary key (UserID)
);
create sequence UserID 
increment by 1
start with 1
cache 20;
comment on table UserInfo is
'�û���Ϣ��';

comment on column UserInfo.UserID is
'�û�ID';

comment on column UserInfo.UniteID is
'����ID';

comment on column UserInfo.LoginType is
'��¼����';

comment on column UserInfo.LoginID is
'��¼ID';

comment on column UserInfo.Name is
'����';

comment on column UserInfo.Email is
'��������';

comment on column UserInfo.Mobile is
'�ֻ�';

comment on column UserInfo.Pwd is
'����';

comment on column UserInfo.InitIdent is
'Ĭ�����';

comment on column UserInfo.DataSource is
'������Դ';

comment on column UserInfo.CreateDate is
'����ʱ��';

comment on column UserInfo.State is
'״ֵ̬';


/*==============================================================*/
/* Table: TokenInfo                                             */
/*==============================================================*/
create table TokenInfo 
(
   TokenID            INT                  not null,
   Token              VARCHAR(100),
   LoginId            VARCHAR(100),
   LoginType          VARCHAR(50),
   UseSys             VARCHAR(50),
   DataSource         VARCHAR(50),
   ClientIP           VARCHAR(20),
   ClientMac          VARCHAR(50),
   LastUpdate         DATE,
   constraint PK_TokenInfo primary key (TokenID)
);
create sequence TokenID 
increment by 1
start with 1
cache 20;
comment on table TokenInfo is
'SSO���Ʊ�';

comment on column TokenInfo.TokenID is
'SKID';

comment on column TokenInfo.Token is
'SessionKey';

comment on column TokenInfo.LoginId is
'��¼��';

comment on column TokenInfo.LoginType is
'��¼����';

comment on column TokenInfo.UseSys is
'ʹ�÷�';

comment on column TokenInfo.DataSource is
'������Դ';

comment on column TokenInfo.ClientIP is
'ClientIP';

comment on column TokenInfo.ClientMac is
'ClientMac';

comment on column TokenInfo.LastUpdate is
'LastUpdate';

/*==============================================================*/
/* Table: Message                                               */
/*==============================================================*/
create table Message 
(
   MsgID              INT                  not null,
   Title              VARCHAR(1000),
   Content            VARCHAR(4000),
   Sender             VARCHAR(20),
   System             INT,
   Creator            VARCHAR(20),
   CreateDate         DATE,
   Modifier           VARCHAR(20),
   ModifyDate         DATE,
   State              INT,
   constraint PK_Message primary key (MsgID)
);
create sequence MsgID 
increment by 1
start with 1
cache 20;
comment on table Message is
'��Ϣ���ݱ�';

comment on column Message.MsgID is
'MsgID';

comment on column Message.Title is
'��������';

comment on column Message.Content is
'��������';

comment on column Message.Sender is
'������';

comment on column Message.System is
'����ϵͳ';

comment on column Message.Creator is
'�����û�';

comment on column Message.CreateDate is
'����ʱ��';

comment on column Message.Modifier is
'�޸���';

comment on column Message.ModifyDate is
'�޸�ʱ��';

comment on column Message.State is
'״ֵ̬';

/*==============================================================*/
/* Table: UserMessage                                           */
/*==============================================================*/
create table UserMessage 
(
   UMID               INT                  not null,
   MsgID              INT,
   Recipient          VARCHAR(100),
   Rtype              VARCHAR(50),
   ReadDate           DATE,
   DelDate            DATE,
   State              INT,
   constraint PK_UserMessage primary key (UMID)
);
create sequence UMID 
increment by 1
start with 1
cache 20;
comment on table UserMessage is
'�û���Ϣ��';

comment on column UserMessage.UMID is
'����ID';

comment on column UserMessage.MsgID is
'MsgID';

comment on column UserMessage.Recipient is
'Recipient';

comment on column UserMessage.Rtype is
'Rtype';

comment on column UserMessage.ReadDate is
'�Ķ�ʱ��';

comment on column UserMessage.DelDate is
'ɾ��ʱ��';

comment on column UserMessage.State is
'״ֵ̬';

/*==============================================================*/
/* Table: Developer                                             */
/*==============================================================*/
create table Developer 
(
   DevID              INT                  not null,
   AccCode            VARCHAR(50),
   DevName            VARCHAR(100),
   DevCode            VARCHAR(50),
   Linkman            VARCHAR(50),
   Mobile             VARCHAR(20),
   Email              VARCHAR(100),
   SiteUrl            VARCHAR(500),
   CallbackUrl        VARCHAR(500),
   LogoutUrl          VARCHAR(500),
   Memo               VARCHAR(2000),
   Creator            INT,
   CreatDate          DATE,
   Modifier           INT,
   ModifyDate         DATE,
   State              INT,
   constraint PK_Developer primary key (DevID)
);
create sequence DevID 
increment by 1
start with 1
cache 20;
comment on table Developer is
'�����߱�';

comment on column Developer.DevID is
'����ID';

comment on column Developer.AccCode is
'��Ȩ���';

comment on column Developer.DevName is
'������';

comment on column Developer.DevCode is
'�������';

comment on column Developer.Linkman is
'��ϵ��';

comment on column Developer.Mobile is
'��ϵ�绰';

comment on column Developer.Email is
'��������';

comment on column Developer.SiteUrl is
'��վ��ַ';

comment on column Developer.CallbackUrl is
'�ص���ַ';

comment on column Developer.LogoutUrl is
'�˳���ַ';

comment on column Developer.Memo is
'��ע';

comment on column Developer.Creator is
'������';

comment on column Developer.CreatDate is
'����ʱ��';

comment on column Developer.Modifier is
'�޸���';

comment on column Developer.ModifyDate is
'�޸�ʱ��';

comment on column Developer.State is
'״̬';

/*==============================================================*/
/* Table: DevAppR                                               */
/*==============================================================*/
create table DevAppR 
(
   DAID               INT                  not null,
   DevID              INT,
   AppID              INT,
   constraint PK_DevAppR primary key (DAID)
);
create sequence DAID 
increment by 1
start with 1
cache 20;
comment on table DevAppR is
'������Ȩ��';

comment on column DevAppR.DAID is
'ID';

comment on column DevAppR.DevID is
'������ID';

comment on column DevAppR.AppID is
'Ӧ��ID';

/*==============================================================*/
/* Table: Application                                           */
/*==============================================================*/
create table Application 
(
   AppID              INT                  not null,
   AppName            VARCHAR(100),
   AppCode            VARCHAR(50),
   PID                INT,
   OrderNum           INT,
   constraint PK_Application primary key (AppID)
);
create sequence AppID 
increment by 1
start with 1
cache 20;
comment on table Application is
'Ӧ�ñ�';

comment on column Application.AppID is
'Ӧ��ID';

comment on column Application.AppName is
'Ӧ������';

comment on column Application.AppCode is
'Ӧ�ñ��';

comment on column Application.PID is
'��ID';

comment on column Application.OrderNum is
'������';

/*==============================================================*/
/* Table: Log                                                   */
/*==============================================================*/
create table Log 
(
   LogID              INT                  not null,
   UserID             INT,
   UserName           VARCHAR(50),
   TypeA              VARCHAR(50),
   TypeB              VARCHAR(50),
   Action             VARCHAR(50),
   Content            VARCHAR(4000),
   ClientIP           VARCHAR(20),
   CreateDate         DATE,
   constraint PK_LOG primary key (LogID)
);
create sequence LogID 
increment by 1
start with 1
cache 20;
comment on table Log is
'��־��';

comment on column Log.LogID is
'����ID';

comment on column Log.UserID is
'�û�ID';

comment on column Log.UserName is
'�û���';

comment on column Log.TypeA is
'�������A';

comment on column Log.TypeB is
'�������B';

comment on column Log.Action is
'����';

comment on column Log.Content is
'��������';

comment on column Log.ClientIP is
'��¼IP';

comment on column Log.CreateDate is
'����ʱ��';