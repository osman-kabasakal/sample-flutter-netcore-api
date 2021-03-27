import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:path/path.dart';
import 'package:sqflite/sqflite.dart';
import 'dart:async';
import 'dart:io';
import 'package:flutter/services.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';

class DatabaseContext {
  AppConfig? appConfig;

  ReactiveSubject<Database>? database;

  DatabaseContext(BuildContext context) {
    this.appConfig = context.getRequireBlocService<AppConfig>();
    this.database = context.getRequireReactiveValue<Database>();
    if (appConfig != null &&
        appConfig!.hasDatabase &&
        this.database != null &&
        this.database!.subject != null) {
      open();
    }
  }

  Future open() async {
    try {
      var databasesPath = await getDatabasesPath();

      var path = join(databasesPath, "sampleProduct.db");

      var exists = await databaseExists(path);

      if (!exists) {
        try {
          await Directory(dirname(path)).create(recursive: true);
        } catch (_) {
          var d = _;
        }
      }
      final database = await openDatabase(
        path,
        version: 1,
        readOnly: false,
        singleInstance: true,
        onUpgrade: databaseOnUpgrade,
        onCreate: (db, version) async {
          return await databaseOnUpgrade(db, 0, 1);
        },
      );
      if (!migrate) {
        // await databaseOnUpgrade(database, 0, 1);
      }
      this.database!.subject!.sink.add(database);
    } catch (e) {
      SystemChannels.platform.invokeMethod('SystemNavigator.pop');
    }
  }

  bool migrate = false;
  final List<List<String>> migrationScripts = [
    [
      // "create table user (id text primary key not null, email text not null unique,jwtToken text,expire integer)",
      '''CREATE TABLE "user" (
	"id"	TEXT NOT NULL UNIQUE,
	"email"	TEXT NOT NULL UNIQUE,
	"jwtToken"	TEXT,
	"expire"	INTEGER,
	PRIMARY KEY("id")
);'''
    ],
  ];
  Future<void> databaseOnUpgrade(
      Database database, int oldversion, int version) async {
    try {
      for (var i = oldversion; i < version; i++) {
        for (var item in migrationScripts[i]) {
          try {
            await database.execute(item);
          } catch (e) {
            continue;
          }
        }
      }
      migrate = true;
      return;
    } catch (e) {
      var d = e;
    }
  }
}
