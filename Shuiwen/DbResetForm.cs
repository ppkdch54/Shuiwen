using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml.Serialization;
using System.IO;

namespace Shuiwen
{
    public partial class DbResetForm : Form
    {
        public DbResetForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            MySqlConnection msc = new MySqlConnection();
            msc.ConnectionString = "Data Source="+ 
                textBoxIp.Text+
                "; User Id="+
                textBoxUser.Text+
                ";Password=" + textBoxPassword.Text+
                ";pooling=false;CharSet=utf8;port="+
                textBoxPort.Text;

            try
            {
                msc.Open();

                MySqlCommand mscmd = msc.CreateCommand();
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandText = @" drop database shuiwen;";
                try
                {
                     mscmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {

                }

                mscmd.CommandText = @"CREATE DATABASE shuiwen;
USE shuiwen;
CREATE TABLE `datas` (
  `num_site` int(11) NOT NULL,
  `num_sensor` int(11) NOT NULL,
  `data` double NOT NULL,
  `time_create` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `TIME_INDEX` (`time_create`),
  KEY `SITE_SENSOR_INDEX` (`num_site`,`num_sensor`)
) ENGINE=InnoDB AUTO_INCREMENT=7071 DEFAULT CHARSET=utf8;

CREATE TABLE `datas_day` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `num_site` int(11) NOT NULL,
  `num_sensor` int(11) NOT NULL,
  `data_sum` double NOT NULL,
  `data_avg` double NOT NULL,
  `data_count` int(11) NOT NULL,
  `time_create` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `time_update` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `QUERY_TIME` (`time_create`,`num_site`,`num_sensor`),
  KEY `QUERY_NUM` (`num_site`,`num_sensor`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

CREATE TABLE `datas_hour` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `num_site` int(11) NOT NULL,
  `num_sensor` int(11) NOT NULL,
  `data_sum` double NOT NULL,
  `data_avg` double NOT NULL,
  `data_count` int(11) NOT NULL,
  `time_create` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `time_update` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `QUERY_TIME` (`time_create`,`num_site`,`num_sensor`),
  KEY `QUERY_NUM` (`num_site`,`num_sensor`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

CREATE TABLE `datas_min` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `num_site` int(11) NOT NULL,
  `num_sensor` int(11) NOT NULL,
  `data_sum` double NOT NULL,
  `data_avg` double NOT NULL,
  `data_count` int(11) NOT NULL,
  `time_create` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `time_update` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `QUERY_TIME` (`time_create`,`num_site`,`num_sensor`),
  KEY `QUERY_NUM` (`num_site`,`num_sensor`)
) ENGINE=InnoDB AUTO_INCREMENT=365 DEFAULT CHARSET=utf8;

CREATE TABLE `datas_month` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `num_site` int(11) NOT NULL,
  `num_sensor` int(11) NOT NULL,
  `data_sum` double NOT NULL,
  `data_avg` double NOT NULL,
  `data_count` int(11) NOT NULL,
  `time_create` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `time_update` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `QUERY_TIME` (`time_create`,`num_site`,`num_sensor`),
  KEY `QUERY_NUM` (`num_site`,`num_sensor`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

CREATE TABLE `sites` (
  `config` longblob NOT NULL,
  `is_new` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `daogui_xishu` (
  `num_daogui` double NOT NULL DEFAULT '1',
  `group_value` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `daogui` (
  `num_site` int(11) NOT NULL,
  `num_sensor` int(11) NOT NULL,
  `inout_value` int(11) NOT NULL,
  `group_value` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE  PROCEDURE `ADD_DATA`(IN site INT,IN sensor INT,IN data DOUBLE,IN weight 
INT,IN curr_time DATETIME )
BEGIN
DECLARE curr DATETIME;
DECLARE curr_min DATETIME;
DECLARE curr_hour DATETIME;
DECLARE curr_day DATETIME;
DECLARE curr_month DATETIME;
DECLARE is_exists INT DEFAULT 0;
DECLARE daogui DOUBLE DEFAULT 1.0;
DECLARE _data DOUBLE;

SET daogui = GET_DAOGUI_XISHU(site,sensor);
SET _data = data*daogui;
SET curr = curr_time;
set curr_min = SUBDATE(curr,INTERVAL SECOND(curr) SECOND);
set curr_hour = SUBDATE(curr_min,INTERVAL MINUTE(curr_min) MINUTE);
set curr_day = SUBDATE(curr_hour,INTERVAL HOUR(curr_hour) HOUR);
set curr_month = SUBDATE(curr_day,INTERVAL DAY(curr_day)-1 DAY);

START TRANSACTION;

SELECT COUNT(*) INTO is_exists FROM datas_min WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_min;

IF is_exists>0 THEN
UPDATE datas_min SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_min;
ELSE 
INSERT INTO datas_min
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_min,curr);
END IF ;

SELECT COUNT(*) INTO is_exists FROM datas_hour WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_hour;

IF is_exists>0 THEN
UPDATE datas_hour SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_hour;
ELSE 
INSERT INTO datas_hour
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_hour,curr);
END IF ;

SELECT COUNT(*) INTO is_exists FROM datas_day WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_day;

IF is_exists>0 THEN
UPDATE datas_day SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_day;
ELSE 
INSERT INTO datas_day
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_day,curr);
END IF ;

SELECT COUNT(*) INTO is_exists FROM datas_month WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_month;

IF is_exists>0 THEN
UPDATE datas_month SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_month;
ELSE 
INSERT INTO datas_month
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_month,curr);
END IF ;

INSERT INTO datas(num_site,num_sensor,data,time_create) VALUES
(site,sensor,_data,curr);

COMMIT;
END;

CREATE  PROCEDURE `_ADD_DATA`(IN site INT,IN sensor INT,IN data DOUBLE,IN weight 
INT,IN curr_time DATETIME )
BEGIN
DECLARE curr DATETIME;
DECLARE curr_min DATETIME;
DECLARE curr_hour DATETIME;
DECLARE curr_day DATETIME;
DECLARE curr_month DATETIME;
DECLARE is_exists INT DEFAULT 0;
DECLARE daogui DOUBLE DEFAULT 1.0;
DECLARE _data DOUBLE;

SET daogui = GET_DAOGUI_XISHU(site,sensor);
SET _data = data*daogui;
SET curr = curr_time;
set curr_min = SUBDATE(curr,INTERVAL SECOND(curr) SECOND);
set curr_hour = SUBDATE(curr_min,INTERVAL MINUTE(curr_min) MINUTE);
set curr_day = SUBDATE(curr_hour,INTERVAL HOUR(curr_hour) HOUR);
set curr_month = SUBDATE(curr_day,INTERVAL DAY(curr_day)-1 DAY);


SELECT COUNT(*) INTO is_exists FROM datas_min WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_min;

IF is_exists>0 THEN
UPDATE datas_min SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_min;
ELSE 
INSERT INTO datas_min
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_min,curr);
END IF ;

SELECT COUNT(*) INTO is_exists FROM datas_hour WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_hour;

IF is_exists>0 THEN
UPDATE datas_hour SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_hour;
ELSE 
INSERT INTO datas_hour
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_hour,curr);
END IF ;

SELECT COUNT(*) INTO is_exists FROM datas_day WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_day;

IF is_exists>0 THEN
UPDATE datas_day SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_day;
ELSE 
INSERT INTO datas_day
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_day,curr);
END IF ;

SELECT COUNT(*) INTO is_exists FROM datas_month WHERE num_site = site AND 
num_sensor=sensor AND time_create = curr_month;

IF is_exists>0 THEN
UPDATE datas_month SET time_update=curr,data_count = data_count+1,data_sum = 
data_sum+(_data*weight),data_avg = data_sum/data_count 
WHERE num_site = site AND num_sensor=sensor AND time_create = curr_month;
ELSE 
INSERT INTO datas_month
(num_site,num_sensor,data_sum,data_avg,data_count,time_create,time_update)
VALUES(site,sensor,_data*weight,_data*weight,1,curr_month,curr);
END IF ;

INSERT INTO datas(num_site,num_sensor,data,time_create) VALUES
(site,sensor,_data,curr);

END;



CREATE PROCEDURE `ADD_RAND_DATA`(count int)
BEGIN
declare i int default 0;
declare curr datetime default NOW();
select max(time_create) into curr from datas;
if ( isnull(curr)) then 
set curr = NOW();
end if;
start transaction;
repeat
set i = i+1;
call _ADD_DATA(1,1,1.5+rand(),3,adddate(curr,INTERVAL 3*i SECOND));
call _ADD_DATA(1,2,2.5+rand(),1,adddate(curr,INTERVAL 3*i SECOND));
call _ADD_DATA(1,3,3.5+rand(),3,adddate(curr,INTERVAL 3*i SECOND));
call _ADD_DATA(1,4,4.5+rand(),1,adddate(curr,INTERVAL 3*i SECOND));
call _ADD_DATA(1,5,5.5+rand(),3,adddate(curr,INTERVAL 3*i SECOND));
call _ADD_DATA(1,6,6.5+rand(),1,adddate(curr,INTERVAL 3*i SECOND));
call _ADD_DATA(1,7,7.5+rand(),1,adddate(curr,INTERVAL 3*i SECOND));
if(i mod 500 = 0) then
commit;
start transaction;
end if;
until i=count end repeat;
commit;
END;
CREATE  FUNCTION `GET_DAOGUI_XISHU`(site INT,sensor INT) RETURNS double
BEGIN
DECLARE ret DOUBLE DEFAULT 1.0;
DECLARE dg_group INT;
SELECT group_value INTO dg_group FROM daogui WHERE num_site = site AND num_sensor = sensor AND inout_value = 2 limit 1;
IF ISNULL(dg_group) THEN 
RETURN 1.0;
END IF;

SELECT num_daogui INTO ret FROM daogui_xishu WHERE group_value = dg_group = site;
IF ISNULL(ret) THEN
RETURN 1.0;
END IF;
RETURN ret;
END;

                    ";
                mscmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

  
}
