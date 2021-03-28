import 'package:flutter/material.dart';
import 'package:sample_products_mobile_app/modules/app/Login/login.dart';

abstract class MainLayout extends StatelessWidget {
  bool hasAppBar = false;

  MainLayout(this.appBar, this.content, this.footer,
      {this.contentSize = 10,
      this.footerSize = 2,
      this.footerActive = true,
      this.hasAppBar = false})
      : super();

  final int contentSize;

  final int footerSize;

  final Widget appBar;

  final Widget content;

  final Widget footer;

  final bool footerActive;

  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async {
        return _onWilProp(context);
      },
      child: Container(
        // padding: EdgeInsets.symmetric(vertical: 15, horizontal: 20),
        decoration: BoxDecoration(color: Colors.grey[200]
            // image: DecorationImage(

            //   image: imagesConstants.backgroundImage,
            //   fit: BoxFit.fill,
            // ),
            ),
        child: SafeArea(
            top: true,
            child: Stack(
              children: [_body(context), Login()],
            )

            //     Builder(
            //   builder: (context) {
            //     var user = context.getRequireReactiveValue<User>();
            //     return StreamBuilder(
            //       stream: user!.subjectStream,
            //       builder: (context, snapshot) {
            //         if (snapshot.connectionState == ConnectionState.done &&
            //             snapshot.hasData) {
            //           return child;
            //         }
            //         var sub = user.subject as BehaviorSubject<User>;
            //         if (snapshot.connectionState != ConnectionState.done &&
            //             sub.valueWrapper?.value == null) {
            //           return Login();
            //         }

            //         return Center(
            //           child: CircularProgressIndicator(),
            //         );
            //       },
            //     );
            //   },
            // ),

            ),
      ),
    );
  }

  Future<bool> _onWilProp(BuildContext context) async {
    return true;
  }

  Widget _body(BuildContext context) {
    // final decorationConstants = Provider.of<DecorationConstants>(context);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        if (this.hasAppBar)
          Expanded(
            flex: 2,
            child: appBar,
          ),
        Expanded(
          child: content,
          flex: this.contentSize,
        ),
        Visibility(
          visible: footerActive,
          child: Expanded(
            child: Container(
              // decoration: decorationConstants.bodyBg,
              child: footer,
            ),
            flex: this.footerSize,
          ),
        ),
      ],
    );
  }
}
