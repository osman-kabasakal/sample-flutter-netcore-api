import 'package:flutter/material.dart';

import 'Config/app_config.dart';
import 'app_starter.dart';
import 'core/bloc/bloc_provider.dart';

void main() {
  runApp(
    BlocProvider(
      bloc: AppConfig(true,
          hasDatabase: true, baseApiUrl: "https://10.0.2.2:15328"),
      child: AppStarter(),
    ),
  );
}
