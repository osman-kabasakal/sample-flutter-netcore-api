import 'dart:async';

import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:sample_products_mobile_app/core/domain/entities/user.dart';

class UserVariable extends ReactiveBehaviorSubjectBloc<User> {
  StreamSubscription<User>? listener;

  UserVariable() {
    listener = subject?.listen((value) {
      // subject?.drain(value);
    });
  }

  @override
  void dispose() {
    listener?.cancel();
    super.dispose();
  }
}
