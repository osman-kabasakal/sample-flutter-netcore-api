import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:path/path.dart';
import 'package:rxdart/rxdart.dart';
import 'package:sample_products_mobile_app/core/domain/entities/user.dart';
import 'package:sample_products_mobile_app/core/domain/repositories/user_repository.dart';
import 'package:sqflite/sqflite.dart';
import 'dart:async';
import 'dart:io';
import 'package:flutter/services.dart';

class DatabaseContext implements ReactiveBehaviorSubjectBloc<Database> {
  DatabaseContext() {}
  @override
  void dispose() {
    subject!.close();
  }

  @override
  Subject<Database>? subject = BehaviorSubject<Database>();

  @override
  Stream<Database> get subjectStream => subject!.stream.asBroadcastStream();

  Future open() async {
    try {
      var databasesPath = await getDatabasesPath();

      var path = join(databasesPath, "sampleProduct.db");

      var exists = await databaseExists(path);

      if (!exists) {
        try {
          await Directory(dirname(path)).create(recursive: true);
        } catch (_) {}

        // Copy from asset
        //TODO: Eğer hazır db varsa onun yolu eklenecek
        ByteData data = await rootBundle.load(join("assets", ""));

        List<int> bytes =
            data.buffer.asUint8List(data.offsetInBytes, data.lengthInBytes);

        await File(path).writeAsBytes(bytes, flush: true);
      } else {}
      final database = await openDatabase(
        path,
        version: 1,
        readOnly: false,
        singleInstance: true,
        onUpgrade: databaseOnUpgrade,
        onCreate: (db, version) async {
          return await databaseOnUpgrade(db, 0, version);
        },
      );
      if (!migrate) {
        //TODO: migrate edilmemiş.
      }
      subject!.sink.add(database);
    } catch (e) {
      SystemChannels.platform.invokeMethod('SystemNavigator.pop');
    }
  }

  bool migrate = false;
  final List<List<String>> migrationScripts = [
    [
      "create table user (id integer primary key not null, email text not null unique,jwtToken text,expire integer)"
    ],
  ];
  Future<void> databaseOnUpgrade(
      Database database, int oldversion, int version) async {
    try {
      for (var i = oldversion; i < version; i++) {
        for (var item in migrationScripts[i]) {
          try {
            await database.rawQuery(item);
          } catch (e) {
            continue;
          }
        }
      }
      migrate = true;
      return;
    } catch (e) {}
  }
}
