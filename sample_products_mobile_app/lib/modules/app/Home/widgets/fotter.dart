import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/constants/route_names.dart';
import 'package:sample_products_mobile_app/core/bloc/reactive_bloc.dart';
import 'package:sample_products_mobile_app/core/domain/entities/user.dart';
import 'package:sample_products_mobile_app/utils/helpers/di_helpers.dart';

class HomeFooter extends StatefulWidget {
  @override
  HomeFooterState createState() => HomeFooterState();
}

class HomeFooterState extends State<HomeFooter> {
  ReactiveSubject<User?>? userReactive;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    this.userReactive = context.getRequireReactiveValue<User>();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.white,
      width: MediaQuery.of(context).size.width,
      height: double.infinity,
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Expanded(
            child: Center(
              child: RawMaterialButton(
                onPressed: () {
                  Navigator.of(context).pushReplacementNamed(Routes.home);
                },
                child: Icon(Icons.home),
              ),
            ),
          ),
          Expanded(
            child: Center(
              child: RawMaterialButton(
                onPressed: () {
                  // userReactive?.subject?.drain();
                  userReactive?.subject?.add(null);
                  Navigator.of(context).pushReplacementNamed(Routes.home);
                },
                child: Icon(Icons.exit_to_app),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
