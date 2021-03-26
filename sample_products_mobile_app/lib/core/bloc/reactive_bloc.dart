import 'package:rxdart/rxdart.dart';

import 'bloc.dart';

abstract class ReactiveSubject<T> extends Bloc {
  Subject<T>? subject;

  Stream<T>? get subjectStream => subject?.stream;
  @override
  void dispose() {
    subject?.close();
  }
}

abstract class ReactiveBehaviorSubjectBloc<T> extends ReactiveSubject<T> {
  @override
  Subject<T>? subject = BehaviorSubject<T>();

  @override
  void dispose() {
    subject?.close();
  }
}

abstract class ReactivePublishSubjectBloc<T> extends ReactiveSubject<T> {
  @override
  Subject<T>? subject = PublishSubject<T>();

  @override
  void dispose() {
    subject?.close();
  }
}

abstract class ReactiveReplaySubjectBloc<T> extends ReactiveSubject<T> {
  ReactiveReplaySubjectBloc({int maxSize = 1}) {
    subject = ReplaySubject(maxSize: maxSize);
  }
  @override
  Subject<T>? subject = ReplaySubject<T>();

  @override
  void dispose() {
    subject?.close();
  }
}

abstract class ReactiveBehaviorSubjectListBloc<T>
    extends ReactiveSubject<List<T>> {
  @override
  Subject<List<T>>? subject = BehaviorSubject<List<T>>();

  @override
  void dispose() {
    subject?.close();
  }
}

abstract class ReactivePublishSubjectListBloc<T>
    extends ReactiveSubject<List<T>> {
  @override
  Subject<List<T>>? subject = PublishSubject<List<T>>();

  @override
  void dispose() {
    subject?.close();
  }
}
