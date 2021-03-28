import 'package:flutter/material.dart';
import 'package:rxdart/subjects.dart';
import 'package:sample_products_mobile_app/core/bloc/bloc_provider.dart';
import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:sample_products_mobile_app/core/domain/entities/user.dart';
import 'package:sample_products_mobile_app/modules/app/Home/blocs/categories.dart';
import 'package:sample_products_mobile_app/modules/app/Home/widgets/fotter.dart';
import 'package:sample_products_mobile_app/modules/app/Home/widgets/home_content.dart';
import 'package:sample_products_mobile_app/modules/layouts/main_screen.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';

class Home extends StatefulWidget {
  @override
  _HomeState createState() => _HomeState();
}

class _HomeState extends State<Home> {
  late ReactiveSubject<User?>? userVariable;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    this.userVariable = context.getRequireReactiveValue<User>();
  }

  @override
  Widget build(BuildContext context) {
    return MainScreen(
      _body(),
      _footer(),
      true,
      contentSize: 13,
      hasAppBar: true,
    );
  }

  Widget _body() => Container(
        // decoration: BoxDecoration(color: Colors.white),
        child: Center(
          child: StreamBuilder(
            stream: userVariable!.subject?.stream,
            builder: (context, snapshot) {
              if (snapshot.hasData) {
                return BlocProvider(
                  bloc: CategoriesBloc(),
                  child: HomeContent(),
                );
              }

              return Center(
                child: CircularProgressIndicator(),
              );
            },
          ),
        ),
      );

  Widget _footer() => HomeFooter();
}
