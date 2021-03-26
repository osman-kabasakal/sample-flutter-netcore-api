import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/Config/app_config.dart';
import 'package:sqflite/sqflite.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';

class RepositoriesLevel extends StatefulWidget {
  final Widget next;
  RepositoriesLevel({required this.next});
  @override
  _RepositoriesLevelState createState() => _RepositoriesLevelState();
}

class _RepositoriesLevelState extends State<RepositoriesLevel> {
  @override
  Widget build(BuildContext context) {
    final db = context.getRequireReactiveValue<Database>();
    final appConfig = context.getRequireBlocService<AppConfig>();
    if (appConfig != null && !appConfig.hasDatabase) {
      return widget.next;
    }
    return StreamBuilder(
      stream: db?.subjectStream,
      builder: (context, snapshot) {
        if (snapshot.hasData) {
          return widget.next;
        }
        return Center(
          child: CircularProgressIndicator(),
        );
      },
    );
  }
}
