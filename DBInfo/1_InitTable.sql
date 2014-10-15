
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
'用户信息表';

comment on column UserInfo.UserID is
'用户ID';

comment on column UserInfo.UniteID is
'联合ID';

comment on column UserInfo.LoginType is
'登录类型';

comment on column UserInfo.LoginID is
'登录ID';

comment on column UserInfo.Name is
'姓名';

comment on column UserInfo.Email is
'电子邮箱';

comment on column UserInfo.Mobile is
'手机';

comment on column UserInfo.Pwd is
'密码';

comment on column UserInfo.InitIdent is
'默认身份';

comment on column UserInfo.DataSource is
'数据来源';

comment on column UserInfo.CreateDate is
'创建时间';

comment on column UserInfo.State is
'状态值';


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
'SSO令牌表';

comment on column TokenInfo.TokenID is
'SKID';

comment on column TokenInfo.Token is
'SessionKey';

comment on column TokenInfo.LoginId is
'登录名';

comment on column TokenInfo.LoginType is
'登录类型';

comment on column TokenInfo.UseSys is
'使用方';

comment on column TokenInfo.DataSource is
'数据来源';

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
'消息内容表';

comment on column Message.MsgID is
'MsgID';

comment on column Message.Title is
'发布标题';

comment on column Message.Content is
'发布内容';

comment on column Message.Sender is
'发送者';

comment on column Message.System is
'发送系统';

comment on column Message.Creator is
'发布用户';

comment on column Message.CreateDate is
'发布时间';

comment on column Message.Modifier is
'修改人';

comment on column Message.ModifyDate is
'修改时间';

comment on column Message.State is
'状态值';

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
'用户消息表';

comment on column UserMessage.UMID is
'主键ID';

comment on column UserMessage.MsgID is
'MsgID';

comment on column UserMessage.Recipient is
'Recipient';

comment on column UserMessage.Rtype is
'Rtype';

comment on column UserMessage.ReadDate is
'阅读时间';

comment on column UserMessage.DelDate is
'删除时间';

comment on column UserMessage.State is
'状态值';

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
'开发者表';

comment on column Developer.DevID is
'开发ID';

comment on column Developer.AccCode is
'授权编号';

comment on column Developer.DevName is
'开发者';

comment on column Developer.DevCode is
'开发编号';

comment on column Developer.Linkman is
'联系人';

comment on column Developer.Mobile is
'联系电话';

comment on column Developer.Email is
'电子邮箱';

comment on column Developer.SiteUrl is
'网站地址';

comment on column Developer.CallbackUrl is
'回调地址';

comment on column Developer.LogoutUrl is
'退出地址';

comment on column Developer.Memo is
'备注';

comment on column Developer.Creator is
'创建人';

comment on column Developer.CreatDate is
'创建时间';

comment on column Developer.Modifier is
'修改人';

comment on column Developer.ModifyDate is
'修改时间';

comment on column Developer.State is
'状态';

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
'开发授权表';

comment on column DevAppR.DAID is
'ID';

comment on column DevAppR.DevID is
'开发者ID';

comment on column DevAppR.AppID is
'应用ID';

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
'应用表';

comment on column Application.AppID is
'应用ID';

comment on column Application.AppName is
'应用名称';

comment on column Application.AppCode is
'应用编号';

comment on column Application.PID is
'父ID';

comment on column Application.OrderNum is
'排序编号';

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
'日志表';

comment on column Log.LogID is
'主键ID';

comment on column Log.UserID is
'用户ID';

comment on column Log.UserName is
'用户名';

comment on column Log.TypeA is
'操作类别A';

comment on column Log.TypeB is
'操作类别B';

comment on column Log.Action is
'动作';

comment on column Log.Content is
'操作内容';

comment on column Log.ClientIP is
'记录IP';

comment on column Log.CreateDate is
'操作时间';