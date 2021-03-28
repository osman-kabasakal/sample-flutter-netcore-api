import 'package:rxdart/rxdart.dart';
import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:sqflite/sqflite.dart';

class DatabaseVariable extends ReactiveBehaviorSubjectBloc<Database> {
  DatabaseVariable() {
    subject?.listen((value) {
      subject?.drain();
    });
  }
  @override
  void dispose() {
    subject!.close();
  }

  @override
  Subject<Database>? subject = BehaviorSubject<Database>();

  @override
  Stream<Database> get subjectStream => subject!.stream.asBroadcastStream();
}
