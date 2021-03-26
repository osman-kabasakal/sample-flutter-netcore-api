import 'package:flutter/material.dart';

class AppBarStLess extends StatelessWidget {
  final String title;
  AppBarStLess(this.title);
  @override
  Widget build(BuildContext context) {
    return Container(
      child: Text(
        title,
        style: Theme.of(context).textTheme.headline1,
      ),
    );
  }
}
