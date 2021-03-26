import 'package:sample_products_mobile_app/core/bloc/bloc.dart';

class AppConfig implements Bloc {
  final bool hasDatabase;

  AppConfig(this.debug, {required this.hasDatabase, required this.baseApiUrl});
  final bool debug;
  final String baseApiUrl;
  @override
  void dispose() {}
}
